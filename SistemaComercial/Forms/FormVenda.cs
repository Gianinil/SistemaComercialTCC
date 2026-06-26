using Microsoft.Data.Sqlite;
using SistemaComercial.Data;
using SistemaComercial.Models;
using SistemaComercial.UI;

namespace SistemaComercial;

public partial class FormVenda : Form
{
    private readonly List<ItemCarrinho> carrinho = new();

    public FormVenda()
    {
        InitializeComponent();
        StartPosition = FormStartPosition.CenterScreen;
    }

    private void FormVenda_Load(object sender, EventArgs e)
    {
        AppTheme.ApplyForm(this, "Vendas");
        AjustarLayout();
        CarregarProdutos();
        CarregarClientes();
        cbMetodoPagamento.Items.Clear();
        cbMetodoPagamento.Items.AddRange(new object[] { "Dinheiro", "Pix" });
        cbMetodoPagamento.SelectedIndex = 0;
        cmbCliente.SelectedIndex = -1;
        cmbProduto.SelectedIndex = -1;
    }

    private void CarregarClientes()
    {
        using var conn = Database.GetConnection();
        conn.Open();
        using var cmd = new SqliteCommand("SELECT Id, Nome FROM Clientes ORDER BY Nome", conn);
        using var reader = cmd.ExecuteReader();
        var lista = new List<ClienteSelecao>();
        while (reader.Read()) lista.Add(new ClienteSelecao { Id = reader.GetInt32(0), Nome = reader.GetString(1) });
        cmbCliente.DataSource = lista;
        cmbCliente.DisplayMember = nameof(ClienteSelecao.Nome);
        cmbCliente.ValueMember = nameof(ClienteSelecao.Id);
    }

    private void CarregarProdutos()
    {
        using var conn = Database.GetConnection();
        conn.Open();
        using var cmd = new SqliteCommand("SELECT Id, Nome FROM Produtos WHERE Quantidade > 0 ORDER BY Nome", conn);
        using var reader = cmd.ExecuteReader();
        var lista = new List<Produto>();
        while (reader.Read()) lista.Add(new Produto { Id = reader.GetInt32(0), Nome = reader.GetString(1) });
        cmbProduto.DataSource = lista;
        cmbProduto.DisplayMember = "Nome";
        cmbProduto.ValueMember = "Id";
    }

    private void btnVender_Click(object sender, EventArgs e)
    {
        if (cmbProduto.SelectedValue == null) { MessageBox.Show("Selecione um produto."); return; }
        if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0) { MessageBox.Show("Quantidade inválida."); return; }
        int produtoId = Convert.ToInt32(cmbProduto.SelectedValue);

        using var conn = Database.GetConnection();
        conn.Open();
        using var cmd = new SqliteCommand("SELECT Preco, Quantidade FROM Produtos WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", produtoId);
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return;
        decimal preco = reader.GetDecimal(0);
        int estoque = reader.GetInt32(1);
        int reservado = carrinho.FirstOrDefault(item => item.ProdutoId == produtoId)?.Quantidade ?? 0;
        if (quantidade + reservado > estoque) { MessageBox.Show("Estoque insuficiente."); return; }

        ItemCarrinho? existente = carrinho.FirstOrDefault(item => item.ProdutoId == produtoId);
        if (existente == null)
        {
            carrinho.Add(new ItemCarrinho
            {
                ProdutoId = produtoId,
                ProdutoNome = cmbProduto.Text,
                Quantidade = quantidade,
                PrecoUnitario = preco
            });
        }
        else existente.Quantidade += quantidade;

        AtualizarCarrinho();
        cmbProduto.SelectedIndex = -1;
        txtQuantidade.Clear();
    }

    private void AtualizarCarrinho()
    {
        dgvCarrinho.DataSource = null;
        dgvCarrinho.DataSource = carrinho.Select(item => new
        {
            Produto = item.ProdutoNome,
            item.Quantidade,
            Unitario = item.PrecoUnitario,
            item.Total
        }).ToList();
        AppTheme.StyleGrid(dgvCarrinho);
        if (dgvCarrinho.Columns["Unitario"] != null) dgvCarrinho.Columns["Unitario"].DefaultCellStyle.Format = "C2";
        if (dgvCarrinho.Columns["Total"] != null) dgvCarrinho.Columns["Total"].DefaultCellStyle.Format = "C2";
        lblTotal.Text = $"Total: {carrinho.Sum(item => item.Total):C2}";
    }

    private void btnRemoverItem_Click(object sender, EventArgs e)
    {
        if (dgvCarrinho.CurrentRow == null || dgvCarrinho.CurrentRow.Index >= carrinho.Count) return;
        carrinho.RemoveAt(dgvCarrinho.CurrentRow.Index);
        AtualizarCarrinho();
    }

    private void btnFinalizar_Click(object sender, EventArgs e)
    {
        if (carrinho.Count == 0) { MessageBox.Show("Carrinho vazio."); return; }
        if (cmbCliente.SelectedValue == null) { MessageBox.Show("Selecione um cliente."); return; }
        string metodo = cbMetodoPagamento.Text;
        decimal totalVenda = carrinho.Sum(item => item.Total);
        int clienteId = Convert.ToInt32(cmbCliente.SelectedValue);
        string clienteNome = cmbCliente.Text;
        string agora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        using var conn = Database.GetConnection();
        conn.Open();
        using var transaction = conn.BeginTransaction();
        try
        {
            int? primeiraVendaId = null;
            foreach (ItemCarrinho item in carrinho)
            {
                using var cmdEstoque = new SqliteCommand(
                    "UPDATE Produtos SET Quantidade = Quantidade - @qtd WHERE Id = @id AND Quantidade >= @qtd", conn, transaction);
                cmdEstoque.Parameters.AddWithValue("@qtd", item.Quantidade);
                cmdEstoque.Parameters.AddWithValue("@id", item.ProdutoId);
                if (cmdEstoque.ExecuteNonQuery() == 0) throw new InvalidOperationException($"Estoque insuficiente para {item.ProdutoNome}.");

                using var cmdVenda = new SqliteCommand(@"
                    INSERT INTO Vendas (ProdutoId, ClienteId, Quantidade, DataVenda, MetodoPagamento)
                    VALUES (@produto, @cliente, @quantidade, @data, @metodo);
                    SELECT last_insert_rowid();", conn, transaction);
                cmdVenda.Parameters.AddWithValue("@produto", item.ProdutoId);
                cmdVenda.Parameters.AddWithValue("@cliente", clienteId);
                cmdVenda.Parameters.AddWithValue("@quantidade", item.Quantidade);
                cmdVenda.Parameters.AddWithValue("@data", agora);
                cmdVenda.Parameters.AddWithValue("@metodo", metodo);
                int vendaId = Convert.ToInt32((long)(cmdVenda.ExecuteScalar() ?? 0L));
                primeiraVendaId ??= vendaId;

                using var cmdMov = new SqliteCommand(@"
                    INSERT INTO EstoqueMovimentacoes
                        (ProdutoId, Tipo, Quantidade, DataMovimento, Origem, OrigemId, Observacao)
                    VALUES (@produto, 'Saida', @quantidade, @data, 'Venda', @venda, @obs)", conn, transaction);
                cmdMov.Parameters.AddWithValue("@produto", item.ProdutoId);
                cmdMov.Parameters.AddWithValue("@quantidade", item.Quantidade);
                cmdMov.Parameters.AddWithValue("@data", agora);
                cmdMov.Parameters.AddWithValue("@venda", vendaId);
                cmdMov.Parameters.AddWithValue("@obs", $"Venda para {clienteNome}");
                cmdMov.ExecuteNonQuery();
            }

            using var cmdConta = new SqliteCommand(@"
                INSERT INTO ContasReceber
                    (Cliente, Valor, Data, Status, Descricao, DataPagamento,
                     MetodoPagamento, Origem, OrigemId)
                VALUES (@cliente, @valor, @data, @status, 'Venda de produtos',
                        @pagamento, @metodo, 'Venda', @venda);
                SELECT last_insert_rowid();", conn, transaction);
            cmdConta.Parameters.AddWithValue("@cliente", clienteNome);
            cmdConta.Parameters.AddWithValue("@valor", totalVenda);
            cmdConta.Parameters.AddWithValue("@data", DateTime.Today.ToString("yyyy-MM-dd"));
            cmdConta.Parameters.AddWithValue("@status", "Pago");
            cmdConta.Parameters.AddWithValue("@pagamento", agora);
            cmdConta.Parameters.AddWithValue("@metodo", metodo);
            cmdConta.Parameters.AddWithValue("@venda", primeiraVendaId ?? 0);
            int contaId = Convert.ToInt32((long)(cmdConta.ExecuteScalar() ?? 0L));

            CaixaService.RegistrarMovimento(conn, transaction, "Entrada", totalVenda,
                "Venda de produtos", metodo, clienteNome, "", "ContaReceber", contaId);

            transaction.Commit();
            MessageBox.Show($"Venda finalizada: {totalVenda:C2}. Entrada registrada no caixa.");
            carrinho.Clear();
            AtualizarCarrinho();
            CarregarProdutos();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            MessageBox.Show(ex.Message, "Não foi possível finalizar a venda");
        }
    }

    private void AjustarLayout()
    {
        int margem = AppTheme.Margin;
        int topo = AppTheme.HeaderHeight + margem;
        panel1.Location = new Point(margem, topo);
        panel1.Size = new Size(300, ClientSize.Height - topo - margem);
        int left = 24, width = panel1.Width - 48, gap = 82, y = 30;
        label2.Location = new Point(left, y); cmbCliente.Location = new Point(left, y + 22); cmbCliente.Size = new Size(width, 26);
        Produto.Location = new Point(left, y + gap); cmbProduto.Location = new Point(left, y + gap + 22); cmbProduto.Size = new Size(width, 26);
        Quantidade.Location = new Point(left, y + gap * 2); txtQuantidade.Location = new Point(left, y + gap * 2 + 22); txtQuantidade.Size = new Size(width, 26);
        label1.Location = new Point(left, y + gap * 3); cbMetodoPagamento.Location = new Point(left, y + gap * 3 + 22); cbMetodoPagamento.Size = new Size(width, 26);
        btnVender.Location = new Point(left, panel1.Height - 76); btnCancelar.Location = new Point(panel1.Width - 144, panel1.Height - 76);
        int contentLeft = panel1.Right + 24;
        dgvCarrinho.Location = new Point(contentLeft, topo);
        dgvCarrinho.Size = new Size(ClientSize.Width - contentLeft - margem, ClientSize.Height - topo - 150);
        lblTotal.Location = new Point(contentLeft, dgvCarrinho.Bottom + 18);
        btnFinalizar.Location = new Point(contentLeft, lblTotal.Bottom + 14);
        btnRemoverItem.Location = new Point(btnFinalizar.Right + 16, lblTotal.Bottom + 14);
    }

    private void btnCancelar_Click(object sender, EventArgs e) => Close();
    private void FormVenda_Paint(object sender, PaintEventArgs e) { }
    private void FormVenda_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Escape) Close(); }
    private void cbMetodoPagamento_SelectedIndexChanged(object sender, EventArgs e) { }
    protected override void OnResize(EventArgs e) { base.OnResize(e); if (IsHandleCreated) AjustarLayout(); }

    private sealed class ClienteSelecao
    {
        public int Id { get; init; }
        public string Nome { get; init; } = string.Empty;
    }
}
