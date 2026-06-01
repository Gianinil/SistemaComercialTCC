using SistemaComercial.Forms;
using SistemaComercial.UI;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;


namespace SistemaComercial
{

    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // Login simples para demonstra??o: valida usu?rio/senha e abre o menu principal.
            if (txtUsuario.Text == "admin" && txtSenha.Text == "123")
            {
                FormMenu menu = new FormMenu();
                menu.StartPosition = FormStartPosition.CenterScreen;
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos!");
            }

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            AplicarLayout();
            panelLogin.Region = Region.FromHrgn(CreateRoundRectRgn(
                0, 0, panelLogin.Width, panelLogin.Height, 30, 30));
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

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

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }
        protected override void OnPaint(PaintEventArgs e)
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

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(
                    this.ActiveControl,
                    true,
                    true,
                    true,
                    true
                );

                e.SuppressKeyPress = true;
            }
        }

        private void AplicarLayout()
        {
            // Aplica a identidade visual tamb?m no login, mantendo consist?ncia antes do menu.
            AppTheme.ApplyForm(this, "Login");
            MinimumSize = new Size(760, 520);
            AjustarLayout();

            panelLogin.BackColor = AppTheme.Card;
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label4.ForeColor = AppTheme.TextMuted;
        }

        private void AjustarLayout()
        {
            // Centraliza o painel de login e distribui os campos de forma responsiva.
            panelLogin.Size = new Size(380, 340);
            panelLogin.Location = new Point(
                (ClientSize.Width - panelLogin.Width) / 2,
                Math.Max(AppTheme.HeaderHeight + AppTheme.Margin, (ClientSize.Height - panelLogin.Height + AppTheme.HeaderHeight) / 2));

            int left = 48;
            int width = panelLogin.Width - left * 2;

            label3.Location = new Point(left, 34);
            label4.Location = new Point(left, 68);
            label1.Location = new Point(left, 118);
            txtUsuario.Location = new Point(left, 140);
            txtUsuario.Size = new Size(width, 28);
            label2.Location = new Point(left, 184);
            txtSenha.Location = new Point(left, 206);
            txtSenha.Size = new Size(width, 28);
            btnEntrar.Location = new Point(left, 270);
            btnEntrar.Size = new Size(width, 44);
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
