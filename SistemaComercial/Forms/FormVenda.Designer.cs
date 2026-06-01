namespace SistemaComercial
{
    partial class FormVenda
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVenda));
            cmbProduto = new ComboBox();
            txtQuantidade = new TextBox();
            btnVender = new Button();
            btnCancelar = new Button();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            Produto = new Label();
            Quantidade = new Label();
            label1 = new Label();
            cbMetodoPagamento = new ComboBox();
            cmbCliente = new ComboBox();
            label2 = new Label();
            panel1 = new Panel();
            dgvCarrinho = new DataGridView();
            btnFinalizar = new Button();
            btnRemoverItem = new Button();
            lblTotal = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCarrinho).BeginInit();
            SuspendLayout();
            // 
            // cmbProduto
            // 
            cmbProduto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProduto.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbProduto.BackColor = Color.White;
            cmbProduto.FlatStyle = FlatStyle.Flat;
            cmbProduto.ForeColor = Color.Black;
            cmbProduto.FormattingEnabled = true;
            cmbProduto.Location = new Point(24, 139);
            cmbProduto.Name = "cmbProduto";
            cmbProduto.Size = new Size(232, 23);
            cmbProduto.TabIndex = 0;
            // 
            // txtQuantidade
            // 
            txtQuantidade.BackColor = Color.White;
            txtQuantidade.ForeColor = Color.Black;
            txtQuantidade.Location = new Point(24, 221);
            txtQuantidade.Name = "txtQuantidade";
            txtQuantidade.Size = new Size(232, 23);
            txtQuantidade.TabIndex = 1;
            // 
            // btnVender
            // 
            btnVender.BackColor = Color.FromArgb(9, 3, 90);
            btnVender.Cursor = Cursors.Hand;
            btnVender.FlatAppearance.BorderSize = 0;
            btnVender.FlatStyle = FlatStyle.Flat;
            btnVender.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnVender.ForeColor = Color.White;
            btnVender.Location = new Point(11, 404);
            btnVender.Name = "btnVender";
            btnVender.Size = new Size(120, 42);
            btnVender.TabIndex = 2;
            btnVender.Text = "Adicionar ao carrinho";
            btnVender.UseVisualStyleBackColor = false;
            btnVender.Click += btnVender_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(9, 3, 90);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(149, 404);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 42);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // Produto
            // 
            Produto.AutoSize = true;
            Produto.BackColor = Color.Transparent;
            Produto.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            Produto.ForeColor = Color.White;
            Produto.Location = new Point(24, 119);
            Produto.Name = "Produto";
            Produto.Size = new Size(58, 17);
            Produto.TabIndex = 4;
            Produto.Text = "Produto";
            // 
            // Quantidade
            // 
            Quantidade.AutoSize = true;
            Quantidade.BackColor = Color.Transparent;
            Quantidade.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            Quantidade.ForeColor = Color.White;
            Quantidade.Location = new Point(24, 201);
            Quantidade.Name = "Quantidade";
            Quantidade.Size = new Size(79, 17);
            Quantidade.TabIndex = 5;
            Quantidade.Text = "Quantidade";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(24, 292);
            label1.Name = "label1";
            label1.Size = new Size(149, 17);
            label1.TabIndex = 6;
            label1.Text = "Método de Pagamento";
            // 
            // cbMetodoPagamento
            // 
            cbMetodoPagamento.BackColor = Color.White;
            cbMetodoPagamento.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMetodoPagamento.FlatStyle = FlatStyle.Flat;
            cbMetodoPagamento.ForeColor = Color.Black;
            cbMetodoPagamento.FormattingEnabled = true;
            cbMetodoPagamento.Location = new Point(24, 312);
            cbMetodoPagamento.Name = "cbMetodoPagamento";
            cbMetodoPagamento.Size = new Size(232, 23);
            cbMetodoPagamento.TabIndex = 7;
            cbMetodoPagamento.SelectedIndexChanged += cbMetodoPagamento_SelectedIndexChanged;
            // 
            // cmbCliente
            // 
            cmbCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCliente.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbCliente.BackColor = Color.White;
            cmbCliente.FlatStyle = FlatStyle.Flat;
            cmbCliente.ForeColor = Color.Black;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(24, 58);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(232, 23);
            cmbCliente.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(24, 38);
            label2.Name = "label2";
            label2.Size = new Size(49, 17);
            label2.TabIndex = 8;
            label2.Text = "Cliente";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(5, 1, 71);
            panel1.Controls.Add(btnVender);
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(txtQuantidade);
            panel1.Controls.Add(cmbProduto);
            panel1.Controls.Add(cmbCliente);
            panel1.Controls.Add(Produto);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(Quantidade);
            panel1.Controls.Add(cbMetodoPagamento);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(20, 20);
            panel1.Name = "panel1";
            panel1.Size = new Size(280, 520);
            panel1.TabIndex = 10;
            // 
            // dgvCarrinho
            // 
            dgvCarrinho.AllowUserToAddRows = false;
            dgvCarrinho.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCarrinho.BackgroundColor = SystemColors.ActiveBorder;
            dgvCarrinho.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCarrinho.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvCarrinho.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvCarrinho.DefaultCellStyle = dataGridViewCellStyle2;
            dgvCarrinho.EnableHeadersVisualStyles = false;
            dgvCarrinho.GridColor = SystemColors.MenuText;
            dgvCarrinho.Location = new Point(320, 20);
            dgvCarrinho.Name = "dgvCarrinho";
            dgvCarrinho.ReadOnly = true;
            dgvCarrinho.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrinho.Size = new Size(540, 380);
            dgvCarrinho.TabIndex = 11;
            // 
            // btnFinalizar
            // 
            btnFinalizar.BackColor = Color.FromArgb(9, 3, 90);
            btnFinalizar.FlatAppearance.BorderSize = 0;
            btnFinalizar.FlatStyle = FlatStyle.Flat;
            btnFinalizar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnFinalizar.ForeColor = Color.White;
            btnFinalizar.Location = new Point(320, 460);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Size = new Size(120, 45);
            btnFinalizar.TabIndex = 12;
            btnFinalizar.Text = "Finalizar venda";
            btnFinalizar.UseVisualStyleBackColor = false;
            btnFinalizar.Click += btnFinalizar_Click;
            // 
            // btnRemoverItem
            // 
            btnRemoverItem.BackColor = Color.FromArgb(9, 3, 90);
            btnRemoverItem.FlatAppearance.BorderSize = 0;
            btnRemoverItem.FlatStyle = FlatStyle.Flat;
            btnRemoverItem.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnRemoverItem.ForeColor = Color.White;
            btnRemoverItem.Location = new Point(540, 460);
            btnRemoverItem.Name = "btnRemoverItem";
            btnRemoverItem.Size = new Size(120, 45);
            btnRemoverItem.TabIndex = 13;
            btnRemoverItem.Text = "Remover item";
            btnRemoverItem.UseVisualStyleBackColor = false;
            btnRemoverItem.Click += btnRemoverItem_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.BackColor = Color.Transparent;
            lblTotal.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.ForeColor = Color.White;
            lblTotal.Location = new Point(320, 415);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(170, 32);
            lblTotal.TabIndex = 14;
            lblTotal.Text = "Total: R$ 0,00";
            // 
            // FormVenda
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 23);
            ClientSize = new Size(884, 561);
            Controls.Add(lblTotal);
            Controls.Add(btnRemoverItem);
            Controls.Add(btnFinalizar);
            Controls.Add(dgvCarrinho);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Location = new Point(20, 20);
            MaximizeBox = false;
            MinimumSize = new Size(900, 600);
            Name = "FormVenda";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Comercial";
            Load += FormVenda_Load;
            Paint += FormVenda_Paint;
            KeyDown += FormVenda_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCarrinho).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbProduto;
        private TextBox txtQuantidade;
        private Button btnVender;
        private Button btnCancelar;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Label Produto;
        private Label Quantidade;
        private Label label1;
        private ComboBox cbMetodoPagamento;
        private ComboBox cmbCliente;
        private Label label2;
        private Panel panel1;
        private DataGridView dgvCarrinho;
        private Button btnFinalizar;
        private Button btnRemoverItem;
        private Label lblTotal;
    }
}