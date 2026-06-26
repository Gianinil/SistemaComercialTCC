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
        private const int SidebarRecolhida = 72;
        private const int SidebarExpandida = 252;
        private readonly System.Windows.Forms.Timer timerSidebar = new();
        private bool expandindo = false;
        private bool sidebarAberta = false;
        private readonly Button btnAlternarSidebar = new();
        private readonly Button btnCompras = CriarBotaoMenu("btnCompras", "Compras");
        private readonly Button btnEstoque = CriarBotaoMenu("btnEstoque", "Estoque");
        private readonly Button btnBackup = CriarBotaoMenu("btnBackup", "Backup");
        private readonly Label lblTituloSidebar = new();
        private readonly Panel indicadorAtivo = new();
        private readonly ToolTip dicasMenu = new();
        private Button? botaoAtivo;

        private static Button CriarBotaoMenu(string nome, string texto)
        {
            return new Button
            {
                Name = nome,
                Text = string.Empty,
                Tag = texto,
                FlatStyle = FlatStyle.Flat,
                ForeColor = AppTheme.Text,
                BackColor = AppTheme.Sidebar,
                Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private static Bitmap CriarIconeMenu(string glifo)
        {
            // Os glifos do Windows viram uma imagem leve e não dependem de arquivos externos.
            var imagem = new Bitmap(28, 28);
            using Graphics graphics = Graphics.FromImage(imagem);
            graphics.Clear(Color.Transparent);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            using var fonte = new Font("Segoe MDL2 Assets", 15F, FontStyle.Regular, GraphicsUnit.Point);
            using var pincel = new SolidBrush(AppTheme.Text);
            SizeF tamanho = graphics.MeasureString(glifo, fonte);
            graphics.DrawString(glifo, fonte, pincel,
                (imagem.Width - tamanho.Width) / 2F,
                (imagem.Height - tamanho.Height) / 2F);
            return imagem;
        }

        private void InicializarSidebar()
        {
            // Timer cria a animação suave de abrir/fechar a sidebar.
            timerSidebar.Interval = 12;
            timerSidebar.Tick += TimerSidebar_Tick;

            btnAlternarSidebar.Name = "btnAlternarSidebar";
            btnAlternarSidebar.Text = "\uE700";
            btnAlternarSidebar.Font = new Font("Segoe MDL2 Assets", 15F);
            btnAlternarSidebar.Size = new Size(40, 40);
            btnAlternarSidebar.BackColor = AppTheme.CardElevated;
            btnAlternarSidebar.ForeColor = AppTheme.Text;
            btnAlternarSidebar.FlatStyle = FlatStyle.Flat;
            btnAlternarSidebar.FlatAppearance.BorderSize = 0;
            btnAlternarSidebar.FlatAppearance.MouseOverBackColor = AppTheme.MenuHover;
            btnAlternarSidebar.FlatAppearance.MouseDownBackColor = AppTheme.Primary;
            btnAlternarSidebar.Cursor = Cursors.Hand;
            btnAlternarSidebar.TextAlign = ContentAlignment.MiddleCenter;
            btnAlternarSidebar.UseVisualStyleBackColor = false;
            btnAlternarSidebar.AccessibleName = "Abrir ou recolher menu lateral";
            dicasMenu.SetToolTip(btnAlternarSidebar, "Abrir ou recolher menu");
            btnAlternarSidebar.Click += (s, e) => AlternarSidebar();

            lblTituloSidebar.Name = "lblTituloSidebar";
            lblTituloSidebar.Text = "MENU PRINCIPAL";
            lblTituloSidebar.AutoSize = true;
            lblTituloSidebar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblTituloSidebar.ForeColor = AppTheme.TextMuted;
            lblTituloSidebar.Visible = false;

            indicadorAtivo.Name = "indicadorAtivo";
            indicadorAtivo.BackColor = AppTheme.Primary;
            indicadorAtivo.Visible = false;

            if (!panel1.Controls.Contains(btnAlternarSidebar))
            {
                panel1.Controls.Add(btnAlternarSidebar);
                panel1.Controls.Add(lblTituloSidebar);
                panel1.Controls.Add(indicadorAtivo);
            }

            panel1.AutoScroll = false;
            panel1.Controls.Add(btnCompras);
            panel1.Controls.Add(btnEstoque);
            panel1.Controls.Add(btnBackup);
            btnCompras.Click += (_, _) => AbrirTelaCentralizada(new FormCompras(), true);
            btnEstoque.Click += (_, _) => AbrirTelaCentralizada(new FormEstoque(), false);
            btnBackup.Click += (_, _) => AbrirTelaCentralizada(new FormBackup(), true);
            ConfigurarBotaoNavegacao(btnProdutos, "Produtos", "\uE7B8");
            ConfigurarBotaoNavegacao(btnVendas, "Vendas", "\uE7BF");
            ConfigurarBotaoNavegacao(btnCompras, "Compras", "\uE719");
            ConfigurarBotaoNavegacao(btnEstoque, "Estoque", "\uE8B7");
            ConfigurarBotaoNavegacao(btnContasReceber, "Contas a receber", "\uE8C7");
            ConfigurarBotaoNavegacao(btnCaixa, "Caixa", "\uEAFD");
            ConfigurarBotaoNavegacao(btnContasPagar, "Contas a pagar", "\uE7C3");
            ConfigurarBotaoNavegacao(button1, "Clientes", "\uE716");
            ConfigurarBotaoNavegacao(btnFornecedor, "Fornecedores", "\uE77B");
            ConfigurarBotaoNavegacao(btnBackup, "Backup", "\uE74E");
            ConfigurarBotaoNavegacao(btnSair, "Sair", "\uE7E8", true);

            Button[] ordemMenu =
            {
                btnAlternarSidebar, btnProdutos, btnVendas, btnCompras, btnEstoque,
                btnContasReceber, btnCaixa, btnContasPagar, button1,
                btnFornecedor, btnBackup, btnSair
            };
            for (int i = 0; i < ordemMenu.Length; i++) ordemMenu[i].TabIndex = i;

            OrganizarBotoesMenu();
            btnAlternarSidebar.BringToFront();
            lblTituloSidebar.BringToFront();
        }

        private void ConfigurarBotaoNavegacao(Button botao, string texto, string glifo, bool perigo = false)
        {
            botao.Tag = texto;
            botao.Text = string.Empty;
            botao.Image = CriarIconeMenu(glifo);
            botao.MinimumSize = Size.Empty;
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;
            botao.FlatAppearance.MouseOverBackColor = perigo
                ? AppTheme.DangerDark
                : AppTheme.MenuHover;
            botao.FlatAppearance.MouseDownBackColor = perigo ? AppTheme.DangerDark : AppTheme.Primary;
            botao.BackColor = AppTheme.Sidebar;
            botao.ForeColor = perigo ? Color.FromArgb(254, 202, 202) : AppTheme.Text;
            botao.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            botao.Cursor = Cursors.Hand;
            botao.AutoEllipsis = true;
            botao.UseVisualStyleBackColor = false;
            botao.AccessibleName = texto;
            dicasMenu.SetToolTip(botao, texto);
            botao.Click += (_, _) => MarcarBotaoAtivo(botao);
        }

        private Button[] ObterBotoesNavegacao() => new[]
        {
            btnProdutos, btnVendas, btnCompras, btnEstoque, btnContasReceber,
            btnCaixa, btnContasPagar, button1, btnFornecedor, btnBackup, btnSair
        };

        private void MarcarBotaoAtivo(Button botao)
        {
            botaoAtivo = botao == btnSair ? null : botao;
            foreach (Button item in ObterBotoesNavegacao())
            {
                item.BackColor = item == botaoAtivo ? AppTheme.MenuActive : AppTheme.Sidebar;
            }

            AtualizarIndicadorAtivo();
        }

        private void AtualizarIndicadorAtivo()
        {
            indicadorAtivo.Visible = botaoAtivo != null;
            if (botaoAtivo == null) return;

            indicadorAtivo.SetBounds(8, botaoAtivo.Top + 9, 3, Math.Max(18, botaoAtivo.Height - 18));
            indicadorAtivo.BringToFront();
        }

        private void AlternarSidebar()
        {
            // Um segundo clique durante a animacao inverte o movimento imediatamente.
            expandindo = timerSidebar.Enabled ? !expandindo : !sidebarAberta;
            timerSidebar.Start();
        }

        private void OrganizarBotoesMenu()
        {
            const int margem = 8;
            const int topo = 72;
            const int espacoItem = 4;
            const int espacoGrupo = 10;
            const int alturaSair = 44;
            int largura = Math.Max(40, panel1.ClientSize.Width - margem * 2);
            int ySair = Math.Max(topo + 430, panel1.ClientSize.Height - alturaSair - 12);
            int disponivel = ySair - topo - espacoItem * 9 - espacoGrupo * 3;
            int alturaBotao = Math.Clamp(disponivel / 10, 36, 46);
            int y = topo;

            PosicionarBotaoMenu(btnProdutos, y, largura, alturaBotao);
            PosicionarBotaoMenu(btnVendas, y += alturaBotao + espacoItem, largura, alturaBotao);
            PosicionarBotaoMenu(btnCompras, y += alturaBotao + espacoItem, largura, alturaBotao);
            PosicionarBotaoMenu(btnEstoque, y += alturaBotao + espacoItem, largura, alturaBotao);

            y += alturaBotao + espacoGrupo;
            PosicionarBotaoMenu(btnContasReceber, y, largura, alturaBotao);
            PosicionarBotaoMenu(btnCaixa, y += alturaBotao + espacoItem, largura, alturaBotao);
            PosicionarBotaoMenu(btnContasPagar, y += alturaBotao + espacoItem, largura, alturaBotao);

            y += alturaBotao + espacoGrupo;
            PosicionarBotaoMenu(button1, y, largura, alturaBotao);
            PosicionarBotaoMenu(btnFornecedor, y += alturaBotao + espacoItem, largura, alturaBotao);

            y += alturaBotao + espacoGrupo;
            PosicionarBotaoMenu(btnBackup, y, largura, alturaBotao);
            PosicionarBotaoMenu(btnSair, ySair, largura, alturaSair);

            btnAlternarSidebar.Location = sidebarAberta
                ? new Point(12, 14)
                : new Point((panel1.Width - btnAlternarSidebar.Width) / 2, 14);
            btnAlternarSidebar.Region?.Dispose();
            using (GraphicsPath formaMenu = GetRoundedRectangle(
                new Rectangle(0, 0, btnAlternarSidebar.Width, btnAlternarSidebar.Height), 6))
            {
                btnAlternarSidebar.Region = new Region(formaMenu);
            }
            lblTituloSidebar.Location = new Point(64, 26);
            AtualizarIndicadorAtivo();
        }

        private void PosicionarBotaoMenu(Button botao, int y, int largura, int altura)
        {
            botao.Location = new Point(8, y);
            botao.Size = new Size(largura, altura);
            botao.Region?.Dispose();
            using GraphicsPath forma = GetRoundedRectangle(new Rectangle(0, 0, largura, altura), 6);
            botao.Region = new Region(forma);
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
                if (panel1.Width < SidebarExpandida)
                    panel1.Width += 12;
                else
                {
                    panel1.Width = SidebarExpandida;
                    sidebarAberta = true;
                    timerSidebar.Stop();
                    MostrarTextosBotoes(true);
                }
            }
            else
            {
                MostrarTextosBotoes(false);
                if (panel1.Width > SidebarRecolhida)
                    panel1.Width -= 12;
                else
                {
                    panel1.Width = SidebarRecolhida;
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
            btnCompras.Text = mostrar ? "  Compras" : "";
            btnEstoque.Text = mostrar ? "  Estoque" : "";
            btnCaixa.Text = mostrar ? "  Caixa" : "";
            btnContasReceber.Text = mostrar ? "  Contas a Receber" : "";
            btnContasPagar.Text = mostrar ? "  Contas a Pagar" : "";
            button1.Text = mostrar ? "  Clientes" : "";
            btnFornecedor.Text = mostrar ? "  Fornecedores" : "";
            btnBackup.Text = mostrar ? "  Backup" : "";
            btnSair.Text = mostrar ? "  Sair" : "";
            lblTituloSidebar.Visible = mostrar;

            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Button btn && btn.Name != "btnAlternarSidebar")
                {
                    btn.ImageAlign = mostrar
                        ? ContentAlignment.MiddleLeft
                        : ContentAlignment.MiddleCenter;
                    btn.TextAlign = mostrar ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleCenter;
                    btn.Padding = mostrar ? new Padding(14, 0, 10, 0) : Padding.Empty;
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
            tela.FormClosed += (_, _) => CarregarResumo();

            if (modal)
            {
                tela.ShowDialog(this);
                CarregarResumo();
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using var borda = new Pen(AppTheme.Border);
            e.Graphics.DrawLine(borda, panel1.ClientSize.Width - 1, 0,
                panel1.ClientSize.Width - 1, panel1.ClientSize.Height);

            using var separador = new Pen(AppTheme.Border);
            int inicio = sidebarAberta ? 16 : 20;
            int fim = sidebarAberta ? panel1.ClientSize.Width - 16 : panel1.ClientSize.Width - 20;
            foreach (int y in new[]
            {
                btnEstoque.Bottom + 5,
                btnContasPagar.Bottom + 5,
                btnFornecedor.Bottom + 5
            })
            {
                e.Graphics.DrawLine(separador, inicio, y, fim, y);
            }
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            // Ordem de carregamento: primeiro visual, depois eventos, layout e dados do dashboard.
            AplicarPaleta();
            InicializarSidebar();
            sidebarAberta = true;
            panel1.Width = SidebarExpandida;
            AjustarPosicaoCards();
            CarregarResumo();
            panel2.Width = this.ClientSize.Width - panel1.Width;
            panel2.Left = panel1.Width;
            MostrarTextosBotoes(true);
            OrganizarBotoesMenu();
        }

        private void AplicarPaleta()
        {
            // Aplica a paleta profissional no menu, onde existem controles fixos do designer.
            AppTheme.ApplyMenuTheme(this, panel1, panel2, dgvUltimasVendas, cardVendas, cardProdutos, cardClientes);
            cardVendas.BackColor = AppTheme.GetDashboardCardColor(nameof(cardVendas));
            cardProdutos.BackColor = AppTheme.GetDashboardCardColor(nameof(cardProdutos));
            cardClientes.BackColor = AppTheme.GetDashboardCardColor(nameof(cardClientes));

            // Painel legado do primeiro layout; a sidebar atual ja ocupa essa area.
            panel3.Visible = false;
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
            AbrirTelaCentralizada(new FormGerenciarFornecedores(), false);
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
            AbrirTelaCentralizada(new FormGerenciarClientes(), false);
        }
    }
}
