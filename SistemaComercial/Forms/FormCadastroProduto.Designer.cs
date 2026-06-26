namespace SistemaComercial.Forms
{
    partial class FormCadastroProduto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCadastroProduto));
            Nome = new Label();
            txtNome = new TextBox();
            txtPreco = new TextBox();
            Preço = new Label();
            Quantidade = new Label();
            txtQuantidade = new TextBox();
            btnSalvar = new Button();
            btnCancelar = new Button();
            dtValidade = new DateTimePicker();
            label1 = new Label();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // Nome
            // 
            Nome.AutoSize = true;
            Nome.BackColor = SystemColors.Control;
            Nome.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            Nome.Location = new Point(266, 75);
            Nome.Name = "Nome";
            Nome.Size = new Size(52, 17);
            Nome.TabIndex = 0;
            Nome.Text = "Nome: ";
            Nome.Click += Nome_Click;
            // 
            // txtNome
            // 
            txtNome.Location = new Point(268, 93);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(242, 23);
            txtNome.TabIndex = 2;
            txtNome.TextChanged += txtNome_TextChanged;
            // 
            // txtPreco
            // 
            txtPreco.Location = new Point(268, 151);
            txtPreco.Name = "txtPreco";
            txtPreco.Size = new Size(242, 23);
            txtPreco.TabIndex = 3;
            // 
            // Preço
            // 
            Preço.AutoSize = true;
            Preço.BackColor = SystemColors.Control;
            Preço.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            Preço.Location = new Point(268, 133);
            Preço.Name = "Preço";
            Preço.Size = new Size(49, 17);
            Preço.TabIndex = 4;
            Preço.Text = "Preço: ";
            // 
            // Quantidade
            // 
            Quantidade.AutoSize = true;
            Quantidade.BackColor = SystemColors.Control;
            Quantidade.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            Quantidade.Location = new Point(268, 194);
            Quantidade.Name = "Quantidade";
            Quantidade.Size = new Size(86, 17);
            Quantidade.TabIndex = 6;
            Quantidade.Text = "Quantidade: ";
            // 
            // txtQuantidade
            // 
            txtQuantidade.Location = new Point(268, 212);
            txtQuantidade.Name = "txtQuantidade";
            txtQuantidade.Size = new Size(242, 23);
            txtQuantidade.TabIndex = 5;
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.FromArgb(5, 1, 71);
            btnSalvar.Cursor = Cursors.Hand;
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalvar.ForeColor = SystemColors.ControlLightLight;
            btnSalvar.Location = new Point(272, 327);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(102, 35);
            btnSalvar.TabIndex = 7;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(5, 1, 71);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = SystemColors.ControlLightLight;
            btnCancelar.Location = new Point(402, 327);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(102, 35);
            btnCancelar.TabIndex = 8;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // dtValidade
            // 
            dtValidade.Format = DateTimePickerFormat.Short;
            dtValidade.Location = new Point(267, 273);
            dtValidade.Name = "dtValidade";
            dtValidade.Size = new Size(242, 23);
            dtValidade.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            label1.Location = new Point(268, 255);
            label1.Name = "label1";
            label1.Size = new Size(65, 17);
            label1.TabIndex = 10;
            label1.Text = "Validade: ";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Location = new Point(227, 49);
            panel1.Name = "panel1";
            panel1.Size = new Size(319, 350);
            panel1.TabIndex = 11;
            // 
            // FormCadastroProduto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 43);
            ClientSize = new Size(824, 487);
            Controls.Add(label1);
            Controls.Add(dtValidade);
            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
            Controls.Add(Quantidade);
            Controls.Add(txtQuantidade);
            Controls.Add(Preço);
            Controls.Add(txtPreco);
            Controls.Add(txtNome);
            Controls.Add(Nome);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            Name = "FormCadastroProduto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Comercial";
            Load += FormCadastroProduto_Load;
            Paint += FormCadastroProduto_Paint;
            KeyDown += FormCadastroProduto_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Nome;
        private TextBox txtNome;
        private TextBox txtPreco;
        private Label Preço;
        private Label Quantidade;
        private TextBox txtQuantidade;
        private Button btnSalvar;
        private Button btnCancelar;
        private DateTimePicker dtValidade;
        private Label label1;
        private Panel panel1;
    }
}