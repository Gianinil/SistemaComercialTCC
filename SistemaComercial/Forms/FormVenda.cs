using Microsoft.Data.Sqlite;
using SistemaComercial.Data;
using SistemaComercial.Models;
using SistemaComercial.UI;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SistemaComercial
{
    public partial class FormVenda : Form
    {
        // Carrinho tempor?rio da venda atual. Ele s? vira venda no banco ao clicar em Finalizar.
        private List<ItemCarrinho> carrinho = new List<ItemCarrinho>();

        public FormVenda()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormVenda_Load(object sender, EventArgs e)
        {
            // Ao abrir a tela, aplica o layout e carrega listas para o usu?rio escolher.
            AplicarLayout();
            CarregarProdutos();
            CarregarClientes();

            cbMetodoPagamento.Items.Add("Dinheiro");
            cbMetodoPagamento.Items.Add("Cartão Débito");
            cbMetodoPagamento.Items.Add("Cartão Crédito");
            cbMetodoPagamento.Items.Add("Pix");
            cbMetodoPagamento.SelectedIndex = 0;
            cmbCliente.SelectedIndex = -1;
            cmbProduto.SelectedIndex = -1;
        }

        public class Cliente
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }

        private void CarregarClientes()
        {
            // Preenche o combo de clientes com os registros cadastrados no banco.
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqliteCommand("SELECT Id, Nome FROM Clientes", conn);
                var reader = cmd.ExecuteReader();
                var lista = new List<Cliente>();
                while (reader.Read())
                    lista.Add(new Cliente { Id = reader.GetInt32(0), Nome = reader.GetString(1) });

                cmbCliente.DataSource = lista;
                cmbCliente.DisplayMember = "Nome";
                cmbCliente.ValueMember = "Id";
            }
        }

        private void CarregarProdutos()
        {
            // Lista apenas produtos com estoque maior que zero para evitar venda imposs?vel.
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqliteCommand("SELECT Id, Nome FROM Produtos WHERE Quantidade > 0", conn);
                var reader = cmd.ExecuteReader();
                var lista = new List<Produto>();
                while (reader.Read())
                    lista.Add(new Produto { Id = reader.GetInt32(0), Nome = reader.GetString(1) });

                cmbProduto.DataSource = lista;
                cmbProduto.DisplayMember = "Nome";
                cmbProduto.ValueMember = "Id";
            }
        }

        // ── ADICIONAR AO CARRINHO ──────────────────────────────────────────
        private void btnVender_Click(object sender, EventArgs e)
        {
            if (cmbProduto.SelectedValue == null) { MessageBox.Show("Selecione um produto."); return; }
            if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0) { MessageBox.Show("Quantidade inválida."); return; }

            int produtoId = (int)cmbProduto.SelectedValue;
            string produtoNome = cmbProduto.Text;

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                var cmd = new SqliteCommand("SELECT Preco, Quantidade FROM Produtos WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", produtoId);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int estoqueAtual = reader.GetInt32(1);
                    decimal preco = reader.GetDecimal(0);

                    int jaNoCarrinho = carrinho.Find(i => i.ProdutoId == produtoId)?.Quantidade ?? 0;

                    if (quantidade + jaNoCarrinho > estoqueAtual)
                    {
                        MessageBox.Show("Estoque insuficiente.");
                        return;
                    }

                    var itemExistente = carrinho.Find(i => i.ProdutoId == produtoId);
                    if (itemExistente != null)
                        itemExistente.Quantidade += quantidade;
                    else
                        carrinho.Add(new ItemCarrinho
                        {
                            ProdutoId = produtoId,
                            ProdutoNome = produtoNome,
                            Quantidade = quantidade,
                            PrecoUnitario = preco
                        });
                }
            }

            AtualizarCarrinho();
            cmbProduto.SelectedIndex = -1;
            txtQuantidade.Clear();
        }

        private void AtualizarCarrinho()
        {
            // Atualiza a grade do carrinho e recalcula o total da venda em tempo real.
            dgvCarrinho.DataSource = null;
            dgvCarrinho.DataSource = carrinho.Select(i => new
            {
                Produto = i.ProdutoNome,
                i.Quantidade,
                Unitário = i.PrecoUnitario.ToString("C2"),
                Total = i.Total.ToString("C2")
            }).ToList();

            lblTotal.Text = $"Total: {carrinho.Sum(i => i.Total):C2}";
        }

        // ── REMOVER ITEM DO CARRINHO ───────────────────────────────────────
        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            if (dgvCarrinho.SelectedRows.Count == 0) return;
            carrinho.RemoveAt(dgvCarrinho.SelectedRows[0].Index);
            AtualizarCarrinho();
        }

        // ── FINALIZAR VENDA ────────────────────────────────────────────────
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (carrinho.Count == 0) { MessageBox.Show("Carrinho vazio."); return; }
            if (cmbCliente.SelectedIndex == -1) { MessageBox.Show("Selecione um cliente."); return; }
            if (cbMetodoPagamento.SelectedItem == null) { MessageBox.Show("Selecione o método de pagamento."); return; }

            string metodo = cbMetodoPagamento.SelectedItem.ToString();
            int clienteId = (int)cmbCliente.SelectedValue;
            string clienteNome = cmbCliente.Text;
            string data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                // Transa??o garante consist?ncia: se algo falhar, nenhuma parte da venda fica gravada pela metade.
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in carrinho)
                        {
                            var cmdEstoque = new SqliteCommand(
                                "UPDATE Produtos SET Quantidade = Quantidade - @qtd WHERE Id = @id", conn, transaction);
                            cmdEstoque.Parameters.AddWithValue("@qtd", item.Quantidade);
                            cmdEstoque.Parameters.AddWithValue("@id", item.ProdutoId);
                            cmdEstoque.ExecuteNonQuery();

                            var cmdVenda = new SqliteCommand(@"
                                INSERT INTO Vendas (ProdutoId, ClienteId, Quantidade, DataVenda, MetodoPagamento)
                                VALUES (@produtoId, @clienteId, @quantidade, @data, @metodo)", conn, transaction);
                            cmdVenda.Parameters.AddWithValue("@produtoId", item.ProdutoId);
                            cmdVenda.Parameters.AddWithValue("@clienteId", clienteId);
                            cmdVenda.Parameters.AddWithValue("@quantidade", item.Quantidade);
                            cmdVenda.Parameters.AddWithValue("@data", data);
                            cmdVenda.Parameters.AddWithValue("@metodo", metodo);
                            cmdVenda.ExecuteNonQuery();

                            var cmdCaixa = new SqliteCommand(@"
                                INSERT INTO Caixa (Tipo, Valor, Descricao, DataMovimento, MetodoPagamento, Cliente)
                                VALUES (@tipo, @valor, @descricao, @data, @metodo, @cliente)", conn, transaction);
                            cmdCaixa.Parameters.AddWithValue("@tipo", "Entrada");
                            cmdCaixa.Parameters.AddWithValue("@valor", item.Total);
                            cmdCaixa.Parameters.AddWithValue("@descricao", $"Venda - {item.ProdutoNome}");
                            cmdCaixa.Parameters.AddWithValue("@data", data);
                            cmdCaixa.Parameters.AddWithValue("@metodo", metodo);
                            cmdCaixa.Parameters.AddWithValue("@cliente", clienteNome);
                            cmdCaixa.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show($"Venda finalizada!\nTotal: {carrinho.Sum(i => i.Total):C2}");
                        carrinho.Clear();
                        AtualizarCarrinho();
                        CarregarProdutos();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Erro ao finalizar venda: " + ex.Message);
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();

        private void FormVenda_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                AppTheme.CardElevated,
                AppTheme.Background,
                90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void FormVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                e.SuppressKeyPress = true;
            }
        }

        private void cbMetodoPagamento_SelectedIndexChanged(object sender, EventArgs e) { }

        private void AplicarLayout()
        {
            // Aplica a identidade visual comum e posiciona os controles da tela de vendas.
            AppTheme.ApplyForm(this, "Vendas");
            AjustarLayout();
        }

        private void AjustarLayout()
        {
            // Responsividade manual do WinForms: painel ? esquerda e carrinho ocupando o restante.
            int margem = AppTheme.Margin;
            int topoConteudo = AppTheme.HeaderHeight + margem;

            panel1.Location = new Point(margem, topoConteudo);
            panel1.Size = new Size(300, ClientSize.Height - topoConteudo - margem);

            int formLeft = 24;
            int inputWidth = panel1.Width - 48;
            int top = 30;
            int gap = 82;

            label2.Location = new Point(formLeft, top);
            cmbCliente.Location = new Point(formLeft, top + 22);
            cmbCliente.Size = new Size(inputWidth, 26);

            Produto.Location = new Point(formLeft, top + gap);
            cmbProduto.Location = new Point(formLeft, top + gap + 22);
            cmbProduto.Size = new Size(inputWidth, 26);

            Quantidade.Location = new Point(formLeft, top + gap * 2);
            txtQuantidade.Location = new Point(formLeft, top + gap * 2 + 22);
            txtQuantidade.Size = new Size(inputWidth, 26);

            label1.Location = new Point(formLeft, top + gap * 3);
            cbMetodoPagamento.Location = new Point(formLeft, top + gap * 3 + 22);
            cbMetodoPagamento.Size = new Size(inputWidth, 26);

            btnVender.Location = new Point(formLeft, panel1.Height - 76);
            btnVender.Size = new Size(120, 44);
            btnCancelar.Location = new Point(panel1.Width - 144, panel1.Height - 76);
            btnCancelar.Size = new Size(120, 44);

            int contentLeft = panel1.Right + 24;
            int contentWidth = ClientSize.Width - contentLeft - margem;

            dgvCarrinho.Location = new Point(contentLeft, topoConteudo);
            dgvCarrinho.Size = new Size(contentWidth, ClientSize.Height - topoConteudo - 150);

            lblTotal.Location = new Point(contentLeft, dgvCarrinho.Bottom + 18);
            btnFinalizar.Location = new Point(contentLeft, lblTotal.Bottom + 14);
            btnFinalizar.Size = new Size(150, 44);
            btnRemoverItem.Location = new Point(btnFinalizar.Right + 16, lblTotal.Bottom + 14);
            btnRemoverItem.Size = new Size(150, 44);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (IsHandleCreated)
            {
                AjustarLayout();
            }
        }

    }
}
