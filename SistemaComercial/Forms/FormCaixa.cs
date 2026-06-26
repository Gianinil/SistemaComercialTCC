using SistemaComercial.Data;
using SistemaComercial.UI;

namespace SistemaComercial;

public partial class FormCaixa : Form
{
    private readonly Button btnAbrir = new() { Name = "btnAbrirCaixa", Text = "ABRIR CAIXA" };
    private readonly Button btnFechar = new() { Name = "btnFecharCaixa", Text = "FECHAR CAIXA" };
    private readonly Label lblStatus = new() { AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };

    public FormCaixa()
    {
        InitializeComponent();
        StartPosition = FormStartPosition.CenterScreen;
        btnAbrir.Click += (_, _) => AbrirCaixa();
        btnFechar.Click += (_, _) => FecharCaixa();
        Controls.Add(btnAbrir);
        Controls.Add(btnFechar);
        Controls.Add(lblStatus);

        btnAbrir.TabIndex = 0;
        btnFechar.TabIndex = 1;
        dgvCaixa.TabIndex = 2;
    }

    private void FormCaixa_Load(object sender, EventArgs e)
    {
        AppTheme.ApplyForm(this, "Caixa do mês");
        AjustarLayout();
        CarregarCaixa();
    }

    private void CarregarCaixa()
    {
        dgvCaixa.DataSource = CaixaService.ListarMovimentosDoMes(out decimal entradas, out decimal saidas);
        AppTheme.StyleGrid(dgvCaixa);
        if (dgvCaixa.Columns["Valor"] != null) dgvCaixa.Columns["Valor"].DefaultCellStyle.Format = "C2";
        lblEntradas.Text = $"Entradas do mês: {entradas:C2}";
        lblSaidas.Text = $"Saídas do mês: {saidas:C2}";
        lblSaldo.Text = $"Resultado do mês: {entradas - saidas:C2}";

        int? sessao = CaixaService.ObterSessaoAberta();
        lblStatus.Text = sessao.HasValue
            ? $"CAIXA ABERTO • Saldo atual: {CaixaService.CalcularSaldoSessao(sessao.Value):C2}"
            : "CAIXA FECHADO";
        lblStatus.ForeColor = sessao.HasValue ? AppTheme.Success : AppTheme.Warning;
        btnAbrir.Enabled = !sessao.HasValue;
        btnFechar.Enabled = sessao.HasValue;
    }

    private void AbrirCaixa()
    {
        string valorTexto = Microsoft.VisualBasic.Interaction.InputBox(
            "Informe o valor inicial disponível no caixa:", "Abertura de caixa", "0,00");
        if (string.IsNullOrWhiteSpace(valorTexto)) return;
        if (!decimal.TryParse(valorTexto, out decimal valor) || valor < 0)
        { MessageBox.Show("Informe um valor inicial válido."); return; }

        try
        {
            CaixaService.Abrir(valor, "Abertura manual");
            MessageBox.Show("Caixa aberto com sucesso.");
            CarregarCaixa();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void FecharCaixa()
    {
        int? sessao = CaixaService.ObterSessaoAberta();
        if (!sessao.HasValue) { MessageBox.Show("Não existe caixa aberto."); return; }
        decimal saldo = CaixaService.CalcularSaldoSessao(sessao.Value);
        if (MessageBox.Show($"O saldo calculado é {saldo:C2}. Deseja fechar o caixa?", "Fechamento",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try
        {
            CaixaService.Fechar("Fechamento manual");
            MessageBox.Show("Caixa fechado com sucesso.");
            CarregarCaixa();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void AjustarLayout()
    {
        int margem = AppTheme.Margin;
        int topo = AppTheme.HeaderHeight + margem + 44;
        lblStatus.Location = new Point(margem, AppTheme.HeaderHeight + 16);
        btnAbrir.Location = new Point(ClientSize.Width - margem - 300, AppTheme.HeaderHeight + 10);
        btnAbrir.Size = new Size(140, 40);
        btnFechar.Location = new Point(ClientSize.Width - margem - 150, AppTheme.HeaderHeight + 10);
        btnFechar.Size = new Size(140, 40);
        dgvCaixa.Location = new Point(margem, topo);
        dgvCaixa.Size = new Size(ClientSize.Width - margem * 2, ClientSize.Height - topo - 86);
        dgvCaixa.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        int y = dgvCaixa.Bottom + 22;
        lblEntradas.Location = new Point(margem, y);
        lblSaidas.Location = new Point(ClientSize.Width / 2 - lblSaidas.Width / 2, y);
        lblSaldo.Location = new Point(ClientSize.Width - margem - lblSaldo.Width, y);
    }

    private void label1_Click(object sender, EventArgs e) { }
    private void dgvCaixa_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    private void FormCaixa_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Escape) Close(); }
    protected override void OnResize(EventArgs e) { base.OnResize(e); if (IsHandleCreated) AjustarLayout(); }
}
