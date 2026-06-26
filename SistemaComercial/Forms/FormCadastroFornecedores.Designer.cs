namespace SistemaComercial
{
    partial class FormCadastroFornecedores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSalvar = new Button();
            label4 = new Label();
            txtEndereco = new TextBox();
            label3 = new Label();
            txtEmail = new TextBox();
            Telefone = new Label();
            txtTelefone = new TextBox();
            label2 = new Label();
            txtCnpj = new TextBox();
            label1 = new Label();
            txtNome = new TextBox();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.FromArgb(9, 3, 90);
            btnSalvar.Cursor = Cursors.Hand;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.ForeColor = SystemColors.Control;
            btnSalvar.Location = new Point(348, 331);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(100, 38);
            btnSalvar.TabIndex = 22;
            btnSalvar.Text = "SALVAR";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Location = new Point(291, 260);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 21;
            label4.Text = "Endereço: ";
            // 
            // txtEndereco
            // 
            txtEndereco.Location = new Point(291, 278);
            txtEndereco.Name = "txtEndereco";
            txtEndereco.Size = new Size(219, 23);
            txtEndereco.TabIndex = 20;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Location = new Point(291, 216);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 19;
            label3.Text = "Email: ";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(291, 234);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(219, 23);
            txtEmail.TabIndex = 18;
            // 
            // Telefone
            // 
            Telefone.AutoSize = true;
            Telefone.BackColor = SystemColors.Control;
            Telefone.Location = new Point(291, 172);
            Telefone.Name = "Telefone";
            Telefone.Size = new Size(58, 15);
            Telefone.TabIndex = 17;
            Telefone.Text = "Telefone: ";
            // 
            // txtTelefone
            // 
            txtTelefone.Location = new Point(291, 190);
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(219, 23);
            txtTelefone.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Location = new Point(291, 128);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 15;
            label2.Text = "CNPJ: ";
            // 
            // txtCnpj
            // 
            txtCnpj.Location = new Point(291, 146);
            txtCnpj.Name = "txtCnpj";
            txtCnpj.Size = new Size(219, 23);
            txtCnpj.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Location = new Point(291, 84);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 13;
            label1.Text = "Nome: ";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(291, 102);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(219, 23);
            txtNome.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Location = new Point(262, 63);
            panel1.Name = "panel1";
            panel1.Size = new Size(276, 325);
            panel1.TabIndex = 23;
            // 
            // FormCadastroFornecedores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSalvar);
            Controls.Add(label4);
            Controls.Add(txtEndereco);
            Controls.Add(label3);
            Controls.Add(txtEmail);
            Controls.Add(Telefone);
            Controls.Add(txtTelefone);
            Controls.Add(label2);
            Controls.Add(txtCnpj);
            Controls.Add(label1);
            Controls.Add(txtNome);
            Controls.Add(panel1);
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormCadastroFornecedores";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormCadastroFornecedores";
            Load += FormCadastroFornecedores_Load;
            Paint += FormCadastroFornecedores_Paint;
            KeyDown += FormCadastroFornecedores_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalvar;
        private Label label4;
        private TextBox txtEndereco;
        private Label label3;
        private TextBox txtEmail;
        private Label Telefone;
        private TextBox txtTelefone;
        private Label label2;
        private TextBox txtCnpj;
        private Label label1;
        private TextBox txtNome;
        private Panel panel1;
    }
}