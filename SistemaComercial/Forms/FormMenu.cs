using Microsoft.Data.Sqlite;
using SistemaComercial.Data;
using SistemaComercial.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace SistemaComercial.Forms
{
    public partial class FormMenu : Form
    {
        // Larguras da barra lateral. Ela começa compacta para economizar espaço
        // e expande ao passar o mouse para mostrar os nomes dos módulos.
        private int sidebarRecolhida = 76;
        private int sidebarExpandida = 220;
        private System.Windows.Forms.Timer timerSidebar = new System.Windows.Forms.Timer();
        private bool expandindo = false;
        private bool sidebarAberta = false;
        private Button btnAlternarSidebar = new Button();

        private void InicializarSidebar()
        {
            // Timer cria a animação suave de abrir/fechar a sidebar.
            timerSidebar.Interval = 10;
            timerSidebar.Tick += TimerSidebar_Tick;

            btnAlternarSidebar.Name = "btnAlternarSidebar";
            btnAlternarSidebar.Text = "\u2630";
            btnAlternarSidebar.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnAlternarSidebar.Size = new Size(52, 48);
            btnAlternarSidebar.Location = new Point(12, 12);
            btnAlternarSidebar.BackColor = AppTheme.Primary;
            btnAlternarSidebar.ForeColor = AppTheme.Text;
            btnAlternarSidebar.FlatStyle = FlatStyle.Flat;
            btnAlternarSidebar.FlatAppearance.BorderSize = 0;
            btnAlternarSidebar.Cursor = Cursors.Hand;
            btnAlternarSidebar.TextAlign = ContentAlignment.MiddleCenter;
            btnAlternarSidebar.UseVisualStyleBackColor = false;
            btnAlternarSidebar.Click += (s, e) => AlternarSidebar();

            if (!panel1.Controls.Contains(btnAlternarSidebar))
            {
                panel1.Controls.Add(btnAlternarSidebar);
                btnAlternarSidebar.BringToFront();
            }

            OrganizarBotoesMenu();
        }

        private void AlternarSidebar()
        {
            expandindo = !sidebarAberta;
            timerSidebar.Start();
        }

        private void OrganizarBotoesMenu()
        {
            int largura = panel1.Width;
            int alturaBotao = 58;
            int y = 88;

            PosicionarBotaoMenu(btnProdutos, y, largura, alturaBotao);
            PosicionarBotaoMenu(btnVendas, y += alturaBotao, largura, alturaBotao);
            PosicionarBotaoMenu(btnContasReceber, y += alturaBotao, largura, alturaBotao);
            PosicionarBotaoMenu(btnCaixa, y += alturaBotao, largura, alturaBotao);
            PosicionarBotaoMenu(btnContasPagar, y += alturaBotao, largura, alturaBotao);
            PosicionarBotaoMenu(button1, y += alturaBotao, largura, alturaBotao);
            PosicionarBotaoMenu(btnFornecedor, y += alturaBotao, largura, alturaBotao);

            btnSair.Location = new Point(0, Math.Max(y + alturaBotao + 12, panel1.ClientSize.Height - alturaBotao - 18));
            btnSair.Size = new Size(largura, alturaBotao);

            btnAlternarSidebar.Location = new Point((panel1.Width - btnAlternarSidebar.Width) / 2, 12);
        }

        private static void PosicionarBotaoMenu(Button botao, int y, int largura, int altura)
        {
            botao.Location = new Point(0, y);
            botao.Size = new Size(largura, altura);
        }

        private void AjustarPosicaoCards()
        {
            if (panel1 == null || cardVendas == null || cardProdutos == null || cardClientes == null || dgvUltimasVendas == null)
            {
                return;
            }

            // Recalcula cards e tabela conforme a largura atual da sidebar.
            // Assim o dashboard fica responsivo quando a janela maximiza ou redimensiona.
            int x = panel1.Width + 20;
            int larguraCard = Math.Max(180, (this.ClientSize.Width - x - 60) / 3);

            cardVendas.Location = new Point(x, 90);
            cardVendas.Width = larguraCard;

            cardProdutos.Location = new Point(x + larguraCard + 20, 90);
            cardProdutos.Width = larguraCard;

            cardClientes.Location = new Point(x + (larguraCard + 20) * 2, 90);
            cardClientes.Width = larguraCard;

            dgvUltimasVendas.Location = new Point(x, 210);
            dgvUltimasVendas.Width = this.ClientSize.Width - x - 20;
        }
        private void CarregarResumo()
        {
            // Busca os indicadores principais para o dashboard:
            // total vendido hoje, total de produtos, total de clientes e últimas vendas.
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                var cmdVendas = new SqliteCommand(
                    "SELECT IFNULL(SUM(v.Quantidade * p.Preco), 0) FROM Vendas v JOIN Produtos p ON v.ProdutoId = p.Id WHERE DATE(v.DataVenda) = DATE('now')", conn);
                decimal totalVendas = Convert.ToDecimal(cmdVendas.ExecuteScalar());
                lblVendas.Text = totalVendas.ToString("C2");

                var cmdProdutos = new SqliteCommand("SELECT COUNT(*) FROM Produtos", conn);
                lblProdutos.Text = cmdProdutos.ExecuteScalar().ToString();

                var cmdClientes = new SqliteCommand("SELECT COUNT(*) FROM Clientes", conn);
                lblClientes.Text = cmdClientes.ExecuteScalar().ToString();

                var cmdGrid = new SqliteCommand(@"
            SELECT c.Nome AS Cliente, p.Nome AS Produto, v.Quantidade, 
                   v.DataVenda AS Data, v.MetodoPagamento AS Pagamento
            FROM Vendas v
            JOIN Produtos p ON v.ProdutoId = p.Id
            LEFT JOIN Clientes c ON v.ClienteId = c.Id
            ORDER BY v.DataVenda DESC LIMIT 20", conn);

                var reader = cmdGrid.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                dgvUltimasVendas.DataSource = dt;

                dgvUltimasVendas.DataSource = dt;

                // Ajusta colunas para preencher largura
                dgvUltimasVendas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Peso de cada coluna (proporção)
                dgvUltimasVendas.Columns["Cliente"].FillWeight = 25;
                dgvUltimasVendas.Columns["Produto"].FillWeight = 25;
                dgvUltimasVendas.Columns["Quantidade"].FillWeight = 10;
                dgvUltimasVendas.Columns["Data"].FillWeight = 25;
                dgvUltimasVendas.Columns["Pagamento"].FillWeight = 15;
            }
        }

        private void TimerSidebar_Tick(object sender, EventArgs e)
        {
            // A cada tick o painel ganha ou perde alguns pixels, criando a animação.
            if (expandindo)
            {
                if (panel1.Width < sidebarExpandida)
                    panel1.Width += 10;
                else
                {
                    panel1.Width = sidebarExpandida;
                    sidebarAberta = true;
                    timerSidebar.Stop();
                    MostrarTextosBotoes(true);
                }
            }
            else
            {
                MostrarTextosBotoes(false);
                if (panel1.Width > sidebarRecolhida)
                    panel1.Width -= 10;
                else
                {
                    panel1.Width = sidebarRecolhida;
                    sidebarAberta = false;
                    timerSidebar.Stop();
                }

            }
            panel2.Width = this.ClientSize.Width - panel1.Width;
            panel2.Left = panel1.Width;
            OrganizarBotoesMenu();
            AjustarPosicaoCards();
        }

        private void MostrarTextosBotoes(bool mostrar)
        {
            // Quando recolhida, a sidebar mostra apenas ícones centralizados.
            // Quando expandida, o texto aparece ao lado do ícone.
            btnProdutos.Text = mostrar ? "  Produtos" : "";
            btnVendas.Text = mostrar ? "  Vendas" : "";
            btnCaixa.Text = mostrar ? "  Caixa" : "";
            btnContasReceber.Text = mostrar ? "  Contas a Receber" : "";
            btnContasPagar.Text = mostrar ? "  Contas a Pagar" : "";
            button1.Text = mostrar ? "  Clientes" : "";
            btnFornecedor.Text = mostrar ? "  Fornecedores" : "";
            btnSair.Text = mostrar ? "  Sair" : "";

            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Button btn && btn.Name != "btnAlternarSidebar")
                {
                    btn.ImageAlign = mostrar
                        ? ContentAlignment.MiddleLeft
                        : ContentAlignment.MiddleCenter;
                    btn.TextAlign = mostrar ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleCenter;
                    btn.Padding = mostrar ? new Padding(18, 0, 8, 0) : Padding.Empty;
                    btn.TextImageRelation = mostrar
                        ? TextImageRelation.ImageBeforeText
                        : TextImageRelation.Overlay;
                }
            }
        }
        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }

        int fadeAlpha = 0;
        System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();

        public FormMenu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            AbrirTelaCentralizada(new FormProdutos(), true);
        }

        private void btnVendas_Click(object sender, EventArgs e)
        {
            AbrirTelaCentralizada(new FormVenda(), true);
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void btnCaixa_Click_1(object sender, EventArgs e)
        {
            AbrirTelaCentralizada(new FormCaixa(), true);
        }

        private void AbrirTelaCentralizada(Form tela, bool modal)
        {
            tela.StartPosition = FormStartPosition.CenterScreen;

            if (modal)
            {
                tela.ShowDialog(this);
                return;
            }

            tela.Show(this);
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e) { }
        private void FormMenu_Paint(object sender, PaintEventArgs e) { }
        private void btnFornecedor_Click(object sender, EventArgs e) { }

        private void btnContasPagar_Click(object sender, EventArgs e)
        {
            AbrirTelaCentralizada(new FormContasPagar(), false);
        }

        private void btnContasReceber_Click(object sender, EventArgs e)
        {
            AbrirTelaCentralizada(new FormContasReceber(), false);
        }

        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            // Ordem de carregamento: primeiro visual, depois eventos, layout e dados do dashboard.
            AplicarPaleta();
            InicializarSidebar();
            panel1.Width = sidebarRecolhida;
            AjustarPosicaoCards();
            CarregarResumo();
            panel2.Width = this.ClientSize.Width - panel1.Width;
            panel2.Left = panel1.Width;
            MostrarTextosBotoes(false);
            OrganizarBotoesMenu();
        }

        private void AplicarPaleta()
        {
            // Aplica a paleta profissional no menu, onde existem controles fixos do designer.
            AppTheme.ApplyMenuTheme(this, panel1, panel2, dgvUltimasVendas, cardVendas, cardProdutos, cardClientes);
            AjustarIconesMenu();

            cardVendas.BackColor = AppTheme.GetDashboardCardColor(nameof(cardVendas));
            cardProdutos.BackColor = AppTheme.GetDashboardCardColor(nameof(cardProdutos));
            cardClientes.BackColor = AppTheme.GetDashboardCardColor(nameof(cardClientes));

            panel3.BackColor = AppTheme.Header;
            lblTituloVendas.ForeColor = AppTheme.TextMuted;
            lblTituloProdutos.ForeColor = AppTheme.TextMuted;
            lblTituloClientes.ForeColor = AppTheme.TextMuted;
            lblVendas.ForeColor = AppTheme.Text;
            lblProdutos.ForeColor = AppTheme.Text;
            lblClientes.ForeColor = AppTheme.Text;
            lblSistema.ForeColor = AppTheme.Text;
            label2.ForeColor = AppTheme.Text;
        }

        private void AjustarIconesMenu()
        {
            // Garante que ícones grandes do designer não fiquem cortados quando a sidebar recolhe.
            foreach (Control control in panel1.Controls)
            {
                if (control is Button button)
                {
                    button.Padding = Padding.Empty;
                    button.ImageAlign = ContentAlignment.MiddleCenter;
                    button.TextAlign = ContentAlignment.MiddleCenter;
                    button.TextImageRelation = TextImageRelation.Overlay;
                    button.MinimumSize = Size.Empty;

                    if (button.Image != null)
                    {
                        button.Image = new Bitmap(button.Image, new Size(26, 26));
                    }
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            // Mantém o dashboard alinhado durante qualquer mudança de tamanho da janela.
            base.OnResize(e);

            if (panel1 == null || panel2 == null)
            {
                return;
            }

            AjustarPosicaoCards();
            panel2.Width = this.ClientSize.Width - panel1.Width;
            panel2.Left = panel1.Width;
            OrganizarBotoesMenu();
        }

        private void btnFornecedor_Click_1(object sender, EventArgs e)
        {
            AbrirTelaCentralizada(new FormCadastroFornecedores(), true);
        }

        private void FormMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                e.SuppressKeyPress = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirTelaCentralizada(new FormClientes(), false);
        }
    }
}
