using SistemaComercial.Forms;
using SistemaComercial.UI;
using System.Drawing.Drawing2D;

namespace SistemaComercial
{
    public partial class FormSplash : Form
    {
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private ProgressBar progressBar;
        private Label lblStatus;
        private int progresso = 0;

        public FormSplash()
        {
            InitializeComponent();
            this.Load += FormSplash_Load;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(500, 300);
            this.BackColor = AppTheme.Background;

            CriarControles();

            timer.Interval = 40;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void CriarControles()
        {
            var lblNome = new Label
            {
                Text = "Sistema Comercial",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = AppTheme.Text,
                AutoSize = true,
                Location = new Point(110, 80)
            };

            var lblSub = new Label
            {
                Text = "Carregando...",
                Font = new Font("Segoe UI", 10),
                ForeColor = AppTheme.TextMuted,
                AutoSize = true,
                Location = new Point(200, 130)
            };
            lblStatus = lblSub;

            progressBar = new ProgressBar
            {
                Location = new Point(50, 220),
                Size = new Size(400, 12),
                Minimum = 0,
                Maximum = 100,
                Style = ProgressBarStyle.Continuous,
                ForeColor = AppTheme.Success
            };

            this.Controls.Add(lblNome);
            this.Controls.Add(lblSub);
            this.Controls.Add(progressBar);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var brush = new LinearGradientBrush(
                this.ClientRectangle,
                AppTheme.Header,
                AppTheme.Background,
                90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progresso += 1;
            progressBar.Value = progresso;

            if (progresso < 30) lblStatus.Text = "Iniciando...";
            else if (progresso < 60) lblStatus.Text = "Carregando banco de dados...";
            else if (progresso < 90) lblStatus.Text = "Preparando interface...";
            else lblStatus.Text = "Pronto!";

            if (progresso >= 100)
            {
                timer.Stop();
                var login = new FormLogin();
                login.FormClosed += (s, e) => Application.Exit();
                login.Show();
                this.Hide();
            }
        }
    }
}
