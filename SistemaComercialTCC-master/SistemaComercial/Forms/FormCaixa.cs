using SistemaComercial.Data.Repositories;
using SistemaComercial.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistemaComercial
{
    public partial class FormCaixa : Form
    {
        public FormCaixa()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormCaixa_Load(object sender, EventArgs e)
        {
            // Ao abrir o caixa, aplica o layout e carrega movimenta??es financeiras.
            AplicarLayout();
            CarregarCaixa();
        }

        private void CarregarCaixa()
        {
            // Exibe entradas, sa?das e saldo usando o resumo calculado pelo reposit?rio.
            var resumo = CaixaRepository.ObterResumo();

            dgvCaixa.DataSource = resumo.Movimentos;
            lblEntradas.Text = "Entradas: R$ " + resumo.TotalEntradas.ToString("N2");
            lblSaidas.Text = "Sa?das: R$ " + resumo.TotalSaidas.ToString("N2");
            lblSaldo.Text = "Saldo: R$ " + resumo.Saldo.ToString("N2");
        }

        private void dgvCaixa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormCaixa_KeyDown(object sender, KeyEventArgs e)
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
            AppTheme.ApplyForm(this, "Caixa");
            AjustarLayout();
        }

        private void AjustarLayout()
        {
            // Mant?m a tabela ocupando o espa?o principal e os totais alinhados no rodap?.
            int margem = AppTheme.Margin;
            int topoConteudo = AppTheme.HeaderHeight + margem;
            int alturaRodape = 82;

            dgvCaixa.Dock = DockStyle.None;
            dgvCaixa.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCaixa.Location = new Point(margem, topoConteudo);
            dgvCaixa.Size = new Size(ClientSize.Width - margem * 2, ClientSize.Height - topoConteudo - alturaRodape - margem);

            int labelY = dgvCaixa.Bottom + 24;
            lblEntradas.Location = new Point(margem, labelY);
            lblSaidas.Location = new Point(Math.Max(margem, ClientSize.Width / 2 - lblSaidas.Width / 2), labelY);
            lblSaldo.Location = new Point(Math.Max(margem, ClientSize.Width - lblSaldo.Width - margem), labelY);
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
