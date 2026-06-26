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

namespace SistemaComercial
{
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ConfigurarValidacoes();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            // Monta o objeto Cliente com os campos digitados e envia para o reposit?rio salvar.
            var cliente = new Cliente
            {
                Nome = txtNome.Text.Trim(),
                CpfCnpj = FormValidation.OnlyDigits(txtCpf.Text),
                Telefone = FormValidation.OnlyDigits(txtTelefone.Text),
                Email = txtEmail.Text.Trim(),
                Endereco = txtEndereco.Text.Trim()
            };

            ClienteRepository.Inserir(cliente);

            MessageBox.Show("Cliente cadastrado com sucesso!");

            // Limpar campos
            txtNome.Clear();
            txtCpf.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            txtEndereco.Clear();
        }

        private void ConfigurarValidacoes()
        {
            txtNome.TabIndex = 0;
            txtCpf.TabIndex = 1;
            txtTelefone.TabIndex = 2;
            txtEmail.TabIndex = 3;
            txtEndereco.TabIndex = 4;
            btnSalvar.TabIndex = 5;
            txtNome.MaxLength = 100;
            txtEndereco.MaxLength = 160;
            FormValidation.ApplyCpfMask(txtCpf);
            FormValidation.ApplyPhoneMask(txtTelefone);
            FormValidation.LimitText(txtEmail, 120);
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do cliente.");
                txtNome.Focus();
                return false;
            }

            if (!FormValidation.IsCpfValid(txtCpf.Text))
            {
                MessageBox.Show("Informe um CPF válido com 11 números.");
                txtCpf.Focus();
                return false;
            }

            if (!FormValidation.IsPhoneValid(txtTelefone.Text))
            {
                MessageBox.Show("Informe um telefone válido com 10 ou 11 números.");
                txtTelefone.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !FormValidation.IsEmailValid(txtEmail.Text))
            {
                MessageBox.Show("O e-mail informado não é válido.");
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void FormClientes_Paint(object sender, PaintEventArgs e)
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

        private void FormClientes_KeyDown(object sender, KeyEventArgs e)
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

        private void FormClientes_Load(object sender, EventArgs e)
        {
            AplicarLayout();
        }

        private void AplicarLayout()
        {
            // Aplica identidade visual e centraliza o formul?rio de cliente.
            AppTheme.ApplyForm(this, "Clientes");
            AjustarLayout();
        }

        private void AjustarLayout()
        {
            // Posiciona os campos dentro do card, mantendo largura e espa?amento consistentes.
            AppTheme.CenterCard(this, panel1, 420, 390);

            int left = panel1.Left + 42;
            int inputWidth = panel1.Width - 84;
            int top = panel1.Top + 30;
            int gap = 50;

            label1.Location = new Point(left, top);
            txtNome.Location = new Point(left, top + 20);
            txtNome.Size = new Size(inputWidth, 26);

            label2.Location = new Point(left, top + gap);
            txtCpf.Location = new Point(left, top + gap + 20);
            txtCpf.Size = new Size(inputWidth, 26);

            Telefone.Location = new Point(left, top + gap * 2);
            txtTelefone.Location = new Point(left, top + gap * 2 + 20);
            txtTelefone.Size = new Size(inputWidth, 26);

            label3.Location = new Point(left, top + gap * 3);
            txtEmail.Location = new Point(left, top + gap * 3 + 20);
            txtEmail.Size = new Size(inputWidth, 26);

            label4.Location = new Point(left, top + gap * 4);
            txtEndereco.Location = new Point(left, top + gap * 4 + 20);
            txtEndereco.Size = new Size(inputWidth, 26);

            btnSalvar.Location = new Point(panel1.Left + (panel1.Width - 140) / 2, panel1.Bottom - 58);
            btnSalvar.Size = new Size(140, 42);

            foreach (Control control in new Control[] { label1, txtNome, label2, txtCpf, Telefone, txtTelefone, label3, txtEmail, label4, txtEndereco, btnSalvar })
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
