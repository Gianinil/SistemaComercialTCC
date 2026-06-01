using SistemaComercial.Data.Repositories;
using SistemaComercial.Forms;
using SistemaComercial.Models;
using SistemaComercial.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace SistemaComercial.Forms
{
    public partial class FormProdutos : Form
    {

        public FormProdutos()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormProdutos_Load(object sender, EventArgs e)
        {
            // Tela de listagem: aplica visual e carrega os produtos cadastrados.
            AplicarLayout();
            AtualizarGrid();
        }
        private void AtualizarGrid()
        {
            // Busca os produtos no reposit?rio e mostra na tabela.
            dgvProdutos.DataSource = ProdutoRepository.Listar();
            ConfigurarGrid();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Novo_Click(object sender, EventArgs e)
        {

        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            // Abre o formul?rio de cadastro em modo novo produto.
            FormCadastroProduto tela = new FormCadastroProduto();
            tela.StartPosition = FormStartPosition.CenterScreen;
            tela.ShowDialog(this);

            AtualizarGrid();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // Exclui o produto selecionado e atualiza a tabela.
            if (dgvProdutos.CurrentRow != null)
            {
                Produto p = (Produto)dgvProdutos.CurrentRow.DataBoundItem;

                try
                {
                    ProdutoRepository.Excluir(p.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                AtualizarGrid();
            }
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            // Abre o mesmo formul?rio de cadastro, mas preenchido para edi??o.
            if (dgvProdutos.CurrentRow != null)
            {
                Produto p = (Produto)dgvProdutos.CurrentRow.DataBoundItem;

                FormCadastroProduto tela = new FormCadastroProduto(p);
                tela.StartPosition = FormStartPosition.CenterScreen;
                tela.ShowDialog(this);

                AtualizarGrid();
            }
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


        private void panelBotoes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // A busca ? feita enquanto o usu?rio digita.
            dgvProdutos.DataSource = ProdutoRepository.Buscar(txtBuscar.Text);
            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            // Ajusta colunas t?cnicas e formata pre?o/data para leitura comercial.
            if (dgvProdutos.Columns["Id"] is DataGridViewColumn idColumn)
            {
                idColumn.Visible = false;
            }

            if (dgvProdutos.Columns["Preco"] is DataGridViewColumn precoColumn)
            {
                precoColumn.DefaultCellStyle.Format = "C2";
            }

            if (dgvProdutos.Columns["Validade"] is DataGridViewColumn validadeColumn)
            {
                validadeColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void AplicarLayout()
        {
            AppTheme.ApplyForm(this, "Produtos");
            AjustarLayout();
        }

        private void AjustarLayout()
        {
            // Posiciona busca, tabela e bot?es conforme o tamanho da janela.
            int margem = AppTheme.Margin;
            int topoConteudo = AppTheme.HeaderHeight + margem;
            int alturaAcoes = 88;
            int alturaBusca = 34;

            panelBotoes.Dock = DockStyle.None;
            panelBotoes.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            panelBotoes.Location = new Point(margem, ClientSize.Height - alturaAcoes - margem);
            panelBotoes.Size = new Size(ClientSize.Width - margem * 2, alturaAcoes);

            int buttonWidth = Math.Min(150, Math.Max(120, panelBotoes.Width / 5));
            Novo.Location = new Point(24, 22);
            Novo.Size = new Size(buttonWidth, 44);
            Editar.Location = new Point((panelBotoes.Width - buttonWidth) / 2, 22);
            Editar.Size = new Size(buttonWidth, 44);
            Excluir.Location = new Point(panelBotoes.Width - buttonWidth - 24, 22);
            Excluir.Size = new Size(buttonWidth, 44);

            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscar.Size = new Size(260, 24);
            txtBuscar.Location = new Point(ClientSize.Width - txtBuscar.Width - margem, topoConteudo);

            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.Location = new Point(txtBuscar.Left - label1.Width - 10, topoConteudo + 3);

            dgvProdutos.Dock = DockStyle.None;
            dgvProdutos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProdutos.Location = new Point(margem, topoConteudo + alturaBusca + 16);
            dgvProdutos.Size = new Size(ClientSize.Width - margem * 2, panelBotoes.Top - dgvProdutos.Top - 16);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (IsHandleCreated)
            {
                AjustarLayout();
            }
        }

        private void FormProdutos_KeyDown(object sender, KeyEventArgs e)
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
    }


}

