namespace SistemaComercial
{
    partial class FormClientes
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
            txtNome = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtCpf = new TextBox();
            Telefone = new Label();
            txtTelefone = new TextBox();
            label3 = new Label();
            txtEmail = new TextBox();
            label4 = new Label();
            txtEndereco = new TextBox();
            btnSalvar = new Button();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // txtNome
            // 
            txtNome.Location = new Point(291, 102);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(219, 23);
            txtNome.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Location = new Point(291, 84);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 1;
            label1.Text = "Nome: ";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Location = new Point(291, 128);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 3;
            label2.Text = "CPF: ";
            // 
            // txtCpf
            // 
            txtCpf.Location = new Point(291, 146);
            txtCpf.Name = "txtCpf";
            txtCpf.Size = new Size(219, 23);
            txtCpf.TabIndex = 2;
            // 
            // Telefone
            // 
            Telefone.AutoSize = true;
            Telefone.BackColor = SystemColors.Control;
            Telefone.Location = new Point(291, 172);
            Telefone.Name = "Telefone";
            Telefone.Size = new Size(58, 15);
            Telefone.TabIndex = 5;
            Telefone.Text = "Telefone: ";
            Telefone.Click += label3_Click;
            // 
            // txtTelefone
            // 
            txtTelefone.Location = new Point(291, 190);
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(219, 23);
            txtTelefone.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Location = new Point(291, 216);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 7;
            label3.Text = "Email: ";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(291, 234);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(219, 23);
            txtEmail.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Location = new Point(291, 260);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 9;
            label4.Text = "Endereço: ";
            // 
            // txtEndereco
            // 
            txtEndereco.Location = new Point(291, 278);
            txtEndereco.Name = "txtEndereco";
            txtEndereco.Size = new Size(219, 23);
            txtEndereco.TabIndex = 8;
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
            btnSalvar.TabIndex = 10;
            btnSalvar.Text = "SALVAR";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Location = new Point(262, 63);
            panel1.Name = "panel1";
            panel1.Size = new Size(276, 325);
            panel1.TabIndex = 11;
            // 
            // FormClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 43);
            ClientSize = new Size(800, 450);
            Controls.Add(btnSalvar);
            Controls.Add(label4);
            Controls.Add(txtEndereco);
            Controls.Add(label3);
            Controls.Add(txtEmail);
            Controls.Add(Telefone);
            Controls.Add(txtTelefone);
            Controls.Add(label2);
            Controls.Add(txtCpf);
            Controls.Add(label1);
            Controls.Add(txtNome);
            Controls.Add(panel1);
            KeyPreview = true;
            Name = "FormClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormClientes";
            Load += FormClientes_Load;
            Paint += FormClientes_Paint;
            KeyDown += FormClientes_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNome;
        private Label label1;
        private Label label2;
        private TextBox txtCpf;
        private Label Telefone;
        private TextBox txtTelefone;
        private Label label3;
        private TextBox txtEmail;
        private Label label4;
        private TextBox txtEndereco;
        private Button btnSalvar;
        private Panel panel1;
    }
}