using SistemaComercial.Data;
using SistemaComercial.UI;

namespace SistemaComercial;

public partial class FormContasReceber : Form
{
    private readonly TextBox txtDescricao = new();
    private readonly ComboBox cbMetodo = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly Button btnEditar = new() { Name = "btnEditar", Text = "EDITAR" };
    private int? contaEdicaoId;

    public FormContasReceber()
    {
        InitializeComponent();
        StartPosition = FormStartPosition.CenterScreen;
        cbMetodo.Items.AddRange(new object[] { "Dinheiro", "Pix" });
        cbMetodo.SelectedIndex = 0;
        btnEditar.Click += (_, _) => PrepararEdicao();
        Controls.Add(txtDescricao);
        Controls.Add(cbMetodo);
        Controls.Add(btnEditar);

        dgvReceber.TabIndex = 0;
        txtCliente.TabIndex = 1;
        txtValor.TabIndex = 2;
        dtData.TabIndex = 3;
        cbStatus.TabIndex = 4;
        txtDescricao.TabIndex = 5;
        cbMetodo.TabIndex = 6;
        btnSalvar.TabIndex = 7;
        btnEditar.TabIndex = 8;
        btnReceber.TabIndex = 9;
    }

    private void dgvReceber_Load(object sender, EventArgs e)
    {
        AppTheme.ApplyForm(this, "Contas a Receber");
        cbStatus.Items.Clear();
        cbStatus.Items.AddRange(new object[] { "Pendente", "Pago" });
        cbStatus.SelectedIndex = 0;
        AjustarLayout();
        Carregar();
    }

    private void Carregar()
    {
        dgvReceber.DataSource = FinanceiroService.Listar(false);
        AppTheme.StyleGrid(dgvReceber);
        if (dgvReceber.Columns["Valor"] != null) dgvReceber.Columns["Valor"].DefaultCellStyle.Format = "C2";
    }

    private void btnSalvar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCliente.Text)) { MessageBox.Show("Informe o cliente."); return; }
        if (!decimal.TryParse(txtValor.Text, out decimal valor) || valor <= 0) { MessageBox.Show("Informe um valor válido."); return; }
        try
        {
            FinanceiroService.Salvar(false, contaEdicaoId, txtCliente.Text.Trim(), valor, dtData.Value,
                cbStatus.Text, txtDescricao.Text.Trim(), cbMetodo.Text);
            MessageBox.Show(contaEdicaoId.HasValue ? "Conta atualizada." : "Conta registrada.");
            Limpar();
            Carregar();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void btnReceber_Click_1(object sender, EventArgs e)
    {
        if (dgvReceber.CurrentRow == null) { MessageBox.Show("Selecione uma conta."); return; }
        try
        {
            int id = Convert.ToInt32(dgvReceber.CurrentRow.Cells["Id"].Value);
            FinanceiroService.Liquidar(false, id, cbMetodo.Text);
            MessageBox.Show("Recebimento registrado no caixa.");
            Carregar();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void PrepararEdicao()
    {
        if (dgvReceber.CurrentRow == null) { MessageBox.Show("Selecione uma conta."); return; }
        contaEdicaoId = Convert.ToInt32(dgvReceber.CurrentRow.Cells["Id"].Value);
        txtCliente.Text = dgvReceber.CurrentRow.Cells["Cliente"].Value?.ToString();
        txtValor.Text = Convert.ToDecimal(dgvReceber.CurrentRow.Cells["Valor"].Value).ToString("N2");
        txtDescricao.Text = dgvReceber.CurrentRow.Cells["Descricao"].Value?.ToString();
        cbStatus.Text = dgvReceber.CurrentRow.Cells["Status"].Value?.ToString();
        cbMetodo.Text = dgvReceber.CurrentRow.Cells["Pagamento"].Value?.ToString();
        if (DateTime.TryParse(dgvReceber.CurrentRow.Cells["Vencimento"].Value?.ToString(), out DateTime data)) dtData.Value = data;
        btnSalvar.Text = "SALVAR ALTERAÇÕES";
    }

    private void Limpar()
    {
        contaEdicaoId = null;
        txtCliente.Clear(); txtValor.Clear(); txtDescricao.Clear();
        cbStatus.SelectedIndex = 0;
        btnSalvar.Text = "SALVAR";
    }

    private void AjustarLayout()
    {
        int margem = AppTheme.Margin;
        int topo = AppTheme.HeaderHeight + margem;
        dgvReceber.Location = new Point(margem, topo);
        dgvReceber.Size = new Size(ClientSize.Width - margem * 2, ClientSize.Height - topo - 190);
        dgvReceber.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        int y = dgvReceber.Bottom + 18;
        label1.Location = new Point(margem, y); txtCliente.Location = new Point(margem, y + 22); txtCliente.Size = new Size(210, 26);
        label2.Location = new Point(margem + 230, y); txtValor.Location = new Point(margem + 230, y + 22); txtValor.Size = new Size(130, 26);
        label3.Text = "Vencimento:"; label3.Location = new Point(margem + 380, y); dtData.Location = new Point(margem + 380, y + 22); dtData.Size = new Size(150, 26);
        label4.Location = new Point(margem + 550, y); cbStatus.Location = new Point(margem + 550, y + 22); cbStatus.Size = new Size(130, 26);
        txtDescricao.PlaceholderText = "Descrição"; txtDescricao.Location = new Point(margem, y + 72); txtDescricao.Size = new Size(360, 26);
        cbMetodo.Location = new Point(margem + 380, y + 72); cbMetodo.Size = new Size(150, 26);
        btnSalvar.Location = new Point(margem + 700, y + 18); btnSalvar.Size = new Size(160, 42);
        btnEditar.Location = new Point(margem + 870, y + 18); btnEditar.Size = new Size(130, 42);
        btnReceber.Location = new Point(ClientSize.Width - margem - 140, y + 18); btnReceber.Size = new Size(140, 42);
    }

    private void dgvReceber_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    private void FormContasReceber_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Escape) Close(); }
    protected override void OnResize(EventArgs e) { base.OnResize(e); if (IsHandleCreated) AjustarLayout(); }
}
