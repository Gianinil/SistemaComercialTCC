using SistemaComercial.Data;
using SistemaComercial.Data.Repositories;
using SistemaComercial.UI;

namespace SistemaComercial;

public partial class FormContasPagar : Form
{
    private readonly TextBox txtDescricao = new();
    private readonly ComboBox cbMetodo = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly Button btnEditar = new() { Name = "btnEditar", Text = "EDITAR" };
    private int? contaEdicaoId;

    public FormContasPagar()
    {
        InitializeComponent();
        StartPosition = FormStartPosition.CenterScreen;
        cbMetodo.Items.AddRange(new object[] { "Dinheiro", "Pix" });
        cbMetodo.SelectedIndex = 0;
        btnEditar.Click += (_, _) => PrepararEdicao();
        Controls.Add(txtDescricao);
        Controls.Add(cbMetodo);
        Controls.Add(btnEditar);

        dgvPagar.TabIndex = 0;
        cbFornecedor.TabIndex = 1;
        txtValor.TabIndex = 2;
        dtData.TabIndex = 3;
        cbStatus.TabIndex = 4;
        txtDescricao.TabIndex = 5;
        cbMetodo.TabIndex = 6;
        btnSalvar.TabIndex = 7;
        btnEditar.TabIndex = 8;
        btnPagar.TabIndex = 9;
    }

    private void FormContasPagar_Load(object sender, EventArgs e)
    {
        AplicarLayout();
        cbStatus.Items.Clear();
        cbStatus.Items.AddRange(new object[] { "Pendente", "Pago" });
        cbStatus.SelectedIndex = 0;
        CarregarFornecedores();
        Carregar();
    }

    private void Carregar()
    {
        dgvPagar.DataSource = FinanceiroService.Listar(true);
        AppTheme.StyleGrid(dgvPagar);
        if (dgvPagar.Columns["Valor"] != null) dgvPagar.Columns["Valor"].DefaultCellStyle.Format = "C2";
    }

    private void CarregarFornecedores()
    {
        cbFornecedor.DataSource = FornecedorRepository.Listar();
        cbFornecedor.DisplayMember = "Nome";
        cbFornecedor.ValueMember = "Id";
        cbFornecedor.SelectedIndex = -1;
    }

    private void btnSalvar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(cbFornecedor.Text)) { MessageBox.Show("Informe o fornecedor."); return; }
        if (!decimal.TryParse(txtValor.Text, out decimal valor) || valor <= 0) { MessageBox.Show("Informe um valor válido."); return; }

        try
        {
            FinanceiroService.Salvar(true, contaEdicaoId, cbFornecedor.Text, valor, dtData.Value,
                cbStatus.Text, txtDescricao.Text.Trim(), cbMetodo.Text);
            MessageBox.Show(contaEdicaoId.HasValue ? "Conta atualizada." : "Conta registrada.");
            Limpar();
            Carregar();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void btnPagar_Click(object sender, EventArgs e)
    {
        if (dgvPagar.CurrentRow == null) { MessageBox.Show("Selecione uma conta."); return; }
        try
        {
            int id = Convert.ToInt32(dgvPagar.CurrentRow.Cells["Id"].Value);
            FinanceiroService.Liquidar(true, id, cbMetodo.Text);
            MessageBox.Show("Pagamento registrado no caixa.");
            Carregar();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void PrepararEdicao()
    {
        if (dgvPagar.CurrentRow == null) { MessageBox.Show("Selecione uma conta."); return; }
        contaEdicaoId = Convert.ToInt32(dgvPagar.CurrentRow.Cells["Id"].Value);
        cbFornecedor.Text = dgvPagar.CurrentRow.Cells["Fornecedor"].Value?.ToString();
        txtValor.Text = Convert.ToDecimal(dgvPagar.CurrentRow.Cells["Valor"].Value).ToString("N2");
        txtDescricao.Text = dgvPagar.CurrentRow.Cells["Descricao"].Value?.ToString();
        cbStatus.Text = dgvPagar.CurrentRow.Cells["Status"].Value?.ToString();
        cbMetodo.Text = dgvPagar.CurrentRow.Cells["Pagamento"].Value?.ToString();
        if (DateTime.TryParse(dgvPagar.CurrentRow.Cells["Vencimento"].Value?.ToString(), out DateTime data)) dtData.Value = data;
        btnSalvar.Text = "SALVAR ALTERAÇÕES";
    }

    private void Limpar()
    {
        contaEdicaoId = null;
        cbFornecedor.SelectedIndex = -1;
        txtValor.Clear();
        txtDescricao.Clear();
        cbStatus.SelectedIndex = 0;
        btnSalvar.Text = "SALVAR";
    }

    private void AplicarLayout()
    {
        AppTheme.ApplyForm(this, "Contas a Pagar");
        AjustarLayout();
    }

    private void AjustarLayout()
    {
        int margem = AppTheme.Margin;
        int topo = AppTheme.HeaderHeight + margem;
        dgvPagar.Location = new Point(margem, topo);
        dgvPagar.Size = new Size(ClientSize.Width - margem * 2, ClientSize.Height - topo - 190);
        dgvPagar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        int y = dgvPagar.Bottom + 18;
        label1.Location = new Point(margem, y); cbFornecedor.Location = new Point(margem, y + 22); cbFornecedor.Size = new Size(210, 26);
        label2.Location = new Point(margem + 230, y); txtValor.Location = new Point(margem + 230, y + 22); txtValor.Size = new Size(130, 26);
        label3.Text = "Vencimento:"; label3.Location = new Point(margem + 380, y); dtData.Location = new Point(margem + 380, y + 22); dtData.Size = new Size(150, 26);
        label4.Location = new Point(margem + 550, y); cbStatus.Location = new Point(margem + 550, y + 22); cbStatus.Size = new Size(130, 26);
        txtDescricao.PlaceholderText = "Descrição"; txtDescricao.Location = new Point(margem, y + 72); txtDescricao.Size = new Size(360, 26);
        cbMetodo.Location = new Point(margem + 380, y + 72); cbMetodo.Size = new Size(150, 26);
        btnSalvar.Location = new Point(margem + 700, y + 18); btnSalvar.Size = new Size(160, 42);
        btnEditar.Location = new Point(margem + 870, y + 18); btnEditar.Size = new Size(130, 42);
        btnPagar.Location = new Point(ClientSize.Width - margem - 140, y + 18); btnPagar.Size = new Size(140, 42);
    }

    private void dgvPagar_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    private void cbStatus_SelectedIndexChanged(object sender, EventArgs e) { }
    private void FormContasPagar_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Escape) Close(); }
    protected override void OnResize(EventArgs e) { base.OnResize(e); if (IsHandleCreated) AjustarLayout(); }
}
