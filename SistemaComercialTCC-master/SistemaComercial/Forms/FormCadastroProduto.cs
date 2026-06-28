using SistemaComercial.Data.Repositories;
using SistemaComercial.Models;
using SistemaComercial.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;


namespace SistemaComercial.Forms
{
    public partial class FormCadastroProduto : Form
    {
        private Produto? produtoEdicao;

        // 🔹 Construtor para NOVO produto
        public FormCadastroProduto()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        // 🔹 Construtor para EDITAR produto
        public FormCadastroProduto(Produto p)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            produtoEdicao = p;

            txtNome.Text = p.Nome;
            txtPreco.Text = p.Preco.ToString();
            txtQuantidade.Text = p.Quantidade.ToString();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormCadastroProduto_Load(object sender, EventArgs e)
        {
            AplicarLayout();
        }

        private void Nome_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Valida os campos e decide se deve inserir um produto novo ou atualizar um existente.
            DateTime validade = dtValidade.Value;

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Digite o nome do produto.");
                return;
            }

            if (!decimal.TryParse(txtPreco.Text, out decimal preco) || preco <= 0)
            {
                MessageBox.Show("Preço inválido.");
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade < 0)
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            if (produtoEdicao == null)
            {
                Produto p = new Produto
                {
                    Nome = txtNome.Text,
                    Preco = preco,
                    Quantidade = quantidade,
                    Validade = validade
                };

                ProdutoRepository.Inserir(p);
            }
            else
            {
                produtoEdicao.Nome = txtNome.Text;
                produtoEdicao.Preco = preco;
                produtoEdicao.Quantidade = quantidade;
                produtoEdicao.Validade = validade;

                ProdutoRepository.Atualizar(produtoEdicao);
            }

            this.Close();
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void FormCadastroProduto_Paint(object sender, PaintEventArgs e)
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

        private void FormCadastroProduto_KeyDown(object sender, KeyEventArgs e)
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
            // Usa o mesmo padr?o visual para cadastro e edi??o de produtos.
            AppTheme.ApplyForm(this, produtoEdicao == null ? "Novo Produto" : "Editar Produto");
            AjustarLayout();
        }

        private void AjustarLayout()
        {
            // Centraliza o card e reposiciona os campos para manter propor??o em qualquer tamanho.
            AppTheme.CenterCard(this, panel1, 420, 410);

            int left = panel1.Left + 42;
            int inputWidth = panel1.Width - 84;
            int top = panel1.Top + 34;
            int fieldGap = 62;

            Nome.Location = new Point(left, top);
            txtNome.Location = new Point(left, top + 22);
            txtNome.Size = new Size(inputWidth, 26);

            Preço.Location = new Point(left, top + fieldGap);
            txtPreco.Location = new Point(left, top + fieldGap + 22);
            txtPreco.Size = new Size(inputWidth, 26);

            Quantidade.Location = new Point(left, top + fieldGap * 2);
            txtQuantidade.Location = new Point(left, top + fieldGap * 2 + 22);
            txtQuantidade.Size = new Size(inputWidth, 26);

            label1.Location = new Point(left, top + fieldGap * 3);
            dtValidade.Location = new Point(left, top + fieldGap * 3 + 22);
            dtValidade.Size = new Size(inputWidth, 26);

            btnSalvar.Location = new Point(left, panel1.Bottom - 68);
            btnSalvar.Size = new Size(130, 42);
            btnCancelar.Location = new Point(panel1.Right - 172, panel1.Bottom - 68);
            btnCancelar.Size = new Size(130, 42);

            foreach (Control control in new Control[] { Nome, txtNome, Preço, txtPreco, Quantidade, txtQuantidade, label1, dtValidade, btnSalvar, btnCancelar })
            {
                control.BringToFront();
            }
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
