namespace SistemaComercial.Forms
{
    partial class FormMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            btnContasReceber = new Button();
            btnContasPagar = new Button();
            btnFornecedor = new Button();
            btnProdutos = new Button();
            btnVendas = new Button();
            btnSair = new Button();
            btnCaixa = new Button();
            timerFade = new System.Windows.Forms.Timer(components);
            panel1 = new Panel();
            button1 = new Button();
            panel3 = new Panel();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            lblVendas = new Label();
            lblProdutos = new Label();
            dgvUltimasVendas = new DataGridView();
            cardVendas = new Panel();
            lblTituloVendas = new Label();
            cardProdutos = new Panel();
            lblTituloProdutos = new Label();
            cardClientes = new Panel();
            lblClientes = new Label();
            lblTituloClientes = new Label();
            label2 = new Label();
            lblSistema = new Label();
            panel2 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUltimasVendas).BeginInit();
            cardVendas.SuspendLayout();
            cardProdutos.SuspendLayout();
            cardClientes.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnContasReceber
            // 
            btnContasReceber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnContasReceber.BackColor = Color.FromArgb(5, 1, 71);
            btnContasReceber.Cursor = Cursors.Hand;
            btnContasReceber.FlatAppearance.BorderSize = 0;
            btnContasReceber.FlatStyle = FlatStyle.Flat;
            btnContasReceber.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnContasReceber.ForeColor = Color.White;
            btnContasReceber.Image = (Image)resources.GetObject("btnContasReceber.Image");
            btnContasReceber.ImageAlign = ContentAlignment.MiddleLeft;
            btnContasReceber.Location = new Point(0, 286);
            btnContasReceber.Margin = new Padding(30);
            btnContasReceber.Name = "btnContasReceber";
            btnContasReceber.Padding = new Padding(10, 8, 10, 8);
            btnContasReceber.Size = new Size(70, 65);
            btnContasReceber.TabIndex = 7;
            btnContasReceber.Text = "CONTAS A RECEBER";
            btnContasReceber.TextAlign = ContentAlignment.MiddleLeft;
            btnContasReceber.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnContasReceber.UseVisualStyleBackColor = false;
            btnContasReceber.Click += btnContasReceber_Click;
            // 
            // btnContasPagar
            // 
            btnContasPagar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnContasPagar.BackColor = Color.FromArgb(5, 1, 71);
            btnContasPagar.Cursor = Cursors.Hand;
            btnContasPagar.FlatAppearance.BorderSize = 0;
            btnContasPagar.FlatStyle = FlatStyle.Flat;
            btnContasPagar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnContasPagar.ForeColor = Color.White;
            btnContasPagar.Image = (Image)resources.GetObject("btnContasPagar.Image");
            btnContasPagar.ImageAlign = ContentAlignment.MiddleLeft;
            btnContasPagar.Location = new Point(0, 416);
            btnContasPagar.Margin = new Padding(30);
            btnContasPagar.Name = "btnContasPagar";
            btnContasPagar.Padding = new Padding(10, 8, 10, 8);
            btnContasPagar.Size = new Size(70, 65);
            btnContasPagar.TabIndex = 6;
            btnContasPagar.Text = "CONTAS A PAGAR";
            btnContasPagar.TextAlign = ContentAlignment.MiddleLeft;
            btnContasPagar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnContasPagar.UseVisualStyleBackColor = false;
            btnContasPagar.Click += btnContasPagar_Click;
            // 
            // btnFornecedor
            // 
            btnFornecedor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnFornecedor.BackColor = Color.FromArgb(5, 1, 71);
            btnFornecedor.Cursor = Cursors.Hand;
            btnFornecedor.FlatAppearance.BorderSize = 0;
            btnFornecedor.FlatStyle = FlatStyle.Flat;
            btnFornecedor.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnFornecedor.ForeColor = Color.White;
            btnFornecedor.Image = (Image)resources.GetObject("btnFornecedor.Image");
            btnFornecedor.ImageAlign = ContentAlignment.MiddleLeft;
            btnFornecedor.Location = new Point(0, 546);
            btnFornecedor.Margin = new Padding(30);
            btnFornecedor.Name = "btnFornecedor";
            btnFornecedor.Padding = new Padding(16, 8, 16, 8);
            btnFornecedor.Size = new Size(70, 65);
            btnFornecedor.TabIndex = 5;
            btnFornecedor.Text = "     CADASTRO DE FORNECEDORES";
            btnFornecedor.TextAlign = ContentAlignment.MiddleLeft;
            btnFornecedor.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnFornecedor.UseVisualStyleBackColor = false;
            btnFornecedor.Click += btnFornecedor_Click_1;
            // 
            // btnProdutos
            // 
            btnProdutos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnProdutos.BackColor = Color.FromArgb(5, 1, 71);
            btnProdutos.Cursor = Cursors.Hand;
            btnProdutos.FlatAppearance.BorderSize = 0;
            btnProdutos.FlatStyle = FlatStyle.Flat;
            btnProdutos.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnProdutos.ForeColor = Color.White;
            btnProdutos.Image = (Image)resources.GetObject("btnProdutos.Image");
            btnProdutos.ImageAlign = ContentAlignment.MiddleLeft;
            btnProdutos.Location = new Point(0, 156);
            btnProdutos.Margin = new Padding(30);
            btnProdutos.Name = "btnProdutos";
            btnProdutos.Padding = new Padding(10, 8, 10, 8);
            btnProdutos.Size = new Size(70, 65);
            btnProdutos.TabIndex = 0;
            btnProdutos.Text = "PRODUTOS";
            btnProdutos.TextAlign = ContentAlignment.MiddleLeft;
            btnProdutos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnProdutos.UseVisualStyleBackColor = false;
            btnProdutos.Click += btnProdutos_Click;
            // 
            // btnVendas
            // 
            btnVendas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnVendas.BackColor = Color.FromArgb(5, 1, 71);
            btnVendas.Cursor = Cursors.Hand;
            btnVendas.FlatAppearance.BorderSize = 0;
            btnVendas.FlatStyle = FlatStyle.Flat;
            btnVendas.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnVendas.ForeColor = Color.White;
            btnVendas.Image = (Image)resources.GetObject("btnVendas.Image");
            btnVendas.ImageAlign = ContentAlignment.MiddleLeft;
            btnVendas.Location = new Point(0, 221);
            btnVendas.Margin = new Padding(30);
            btnVendas.Name = "btnVendas";
            btnVendas.Padding = new Padding(16, 8, 16, 8);
            btnVendas.Size = new Size(70, 65);
            btnVendas.TabIndex = 2;
            btnVendas.Text = "VENDAS";
            btnVendas.TextAlign = ContentAlignment.MiddleLeft;
            btnVendas.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnVendas.UseVisualStyleBackColor = false;
            btnVendas.Click += btnVendas_Click;
            // 
            // btnSair
            // 
            btnSair.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnSair.BackColor = Color.FromArgb(5, 1, 71);
            btnSair.Cursor = Cursors.Hand;
            btnSair.FlatAppearance.BorderSize = 0;
            btnSair.FlatStyle = FlatStyle.Flat;
            btnSair.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnSair.ForeColor = Color.White;
            btnSair.ImageAlign = ContentAlignment.MiddleLeft;
            btnSair.Location = new Point(0, 656);
            btnSair.Margin = new Padding(30);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(70, 65);
            btnSair.TabIndex = 1;
            btnSair.Text = "SAIR";
            btnSair.TextAlign = ContentAlignment.MiddleLeft;
            btnSair.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSair.UseVisualStyleBackColor = false;
            btnSair.Click += btnSair_Click;
            // 
            // btnCaixa
            // 
            btnCaixa.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnCaixa.BackColor = Color.FromArgb(5, 1, 71);
            btnCaixa.Cursor = Cursors.Hand;
            btnCaixa.FlatAppearance.BorderSize = 0;
            btnCaixa.FlatStyle = FlatStyle.Flat;
            btnCaixa.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            btnCaixa.ForeColor = Color.White;
            btnCaixa.Image = (Image)resources.GetObject("btnCaixa.Image");
            btnCaixa.ImageAlign = ContentAlignment.MiddleLeft;
            btnCaixa.Location = new Point(0, 351);
            btnCaixa.Margin = new Padding(30);
            btnCaixa.Name = "btnCaixa";
            btnCaixa.Padding = new Padding(16, 8, 16, 8);
            btnCaixa.Size = new Size(70, 65);
            btnCaixa.TabIndex = 3;
            btnCaixa.Text = "CAIXA";
            btnCaixa.TextAlign = ContentAlignment.MiddleLeft;
            btnCaixa.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCaixa.UseVisualStyleBackColor = false;
            btnCaixa.Click += btnCaixa_Click_1;
            // 
            // timerFade
            // 
            timerFade.Interval = 20;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 0, 43);
            panel1.Controls.Add(btnSair);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(btnFornecedor);
            panel1.Controls.Add(btnContasPagar);
            panel1.Controls.Add(btnContasReceber);
            panel1.Controls.Add(btnCaixa);
            panel1.Controls.Add(btnVendas);
            panel1.Controls.Add(btnProdutos);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(70, 761);
            panel1.TabIndex = 8;
            panel1.Paint += panel1_Paint;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(5, 1, 71);
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(0, 481);
            button1.Margin = new Padding(30);
            button1.Name = "button1";
            button1.Padding = new Padding(16, 8, 16, 8);
            button1.Size = new Size(70, 65);
            button1.TabIndex = 6;
            button1.Text = "     CADASTRO DE CLIENTES";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(9, 3, 90);
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(70, 100);
            panel3.TabIndex = 0;
            // 
            // lblVendas
            // 
            lblVendas.AutoSize = true;
            lblVendas.BackColor = Color.Transparent;
            lblVendas.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblVendas.ForeColor = Color.White;
            lblVendas.Location = new Point(15, 45);
            lblVendas.Name = "lblVendas";
            lblVendas.Size = new Size(83, 32);
            lblVendas.TabIndex = 13;
            lblVendas.Text = "label4";
            // 
            // lblProdutos
            // 
            lblProdutos.AutoSize = true;
            lblProdutos.BackColor = Color.Transparent;
            lblProdutos.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblProdutos.ForeColor = Color.White;
            lblProdutos.Location = new Point(15, 45);
            lblProdutos.Name = "lblProdutos";
            lblProdutos.RightToLeft = RightToLeft.Yes;
            lblProdutos.Size = new Size(83, 32);
            lblProdutos.TabIndex = 14;
            lblProdutos.Text = "label5";
            // 
            // dgvUltimasVendas
            // 
            dgvUltimasVendas.AllowUserToAddRows = false;
            dgvUltimasVendas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUltimasVendas.BackgroundColor = Color.FromArgb(5, 1, 71);
            dgvUltimasVendas.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(9, 3, 90);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvUltimasVendas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvUltimasVendas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(5, 1, 71);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvUltimasVendas.DefaultCellStyle = dataGridViewCellStyle4;
            dgvUltimasVendas.GridColor = Color.FromArgb(9, 3, 90);
            dgvUltimasVendas.Location = new Point(70, 200);
            dgvUltimasVendas.Name = "dgvUltimasVendas";
            dgvUltimasVendas.ReadOnly = true;
            dgvUltimasVendas.RowHeadersVisible = false;
            dgvUltimasVendas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUltimasVendas.Size = new Size(1370, 500);
            dgvUltimasVendas.TabIndex = 16;
            // 
            // cardVendas
            // 
            cardVendas.BackColor = Color.FromArgb(5, 1, 71);
            cardVendas.Controls.Add(lblTituloVendas);
            cardVendas.Controls.Add(lblVendas);
            cardVendas.Location = new Point(90, 120);
            cardVendas.Name = "cardVendas";
            cardVendas.Size = new Size(250, 100);
            cardVendas.TabIndex = 17;
            // 
            // lblTituloVendas
            // 
            lblTituloVendas.AutoSize = true;
            lblTituloVendas.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTituloVendas.ForeColor = Color.FromArgb(150, 150, 200);
            lblTituloVendas.Location = new Point(10, 10);
            lblTituloVendas.Name = "lblTituloVendas";
            lblTituloVendas.Size = new Size(89, 20);
            lblTituloVendas.TabIndex = 14;
            lblTituloVendas.Text = "Vendas hoje";
            // 
            // cardProdutos
            // 
            cardProdutos.BackColor = Color.FromArgb(5, 1, 71);
            cardProdutos.Controls.Add(lblTituloProdutos);
            cardProdutos.Controls.Add(lblProdutos);
            cardProdutos.Location = new Point(360, 120);
            cardProdutos.Name = "cardProdutos";
            cardProdutos.Size = new Size(250, 100);
            cardProdutos.TabIndex = 18;
            // 
            // lblTituloProdutos
            // 
            lblTituloProdutos.AutoSize = true;
            lblTituloProdutos.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTituloProdutos.ForeColor = Color.FromArgb(150, 150, 200);
            lblTituloProdutos.Location = new Point(10, 10);
            lblTituloProdutos.Name = "lblTituloProdutos";
            lblTituloProdutos.Size = new Size(68, 20);
            lblTituloProdutos.TabIndex = 15;
            lblTituloProdutos.Text = "Produtos";
            // 
            // cardClientes
            // 
            cardClientes.BackColor = Color.FromArgb(5, 1, 71);
            cardClientes.Controls.Add(lblClientes);
            cardClientes.Controls.Add(lblTituloClientes);
            cardClientes.Location = new Point(630, 120);
            cardClientes.Name = "cardClientes";
            cardClientes.Size = new Size(250, 100);
            cardClientes.TabIndex = 19;
            // 
            // lblClientes
            // 
            lblClientes.AutoSize = true;
            lblClientes.BackColor = Color.Transparent;
            lblClientes.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblClientes.ForeColor = Color.White;
            lblClientes.Location = new Point(15, 45);
            lblClientes.Name = "lblClientes";
            lblClientes.RightToLeft = RightToLeft.Yes;
            lblClientes.Size = new Size(83, 32);
            lblClientes.TabIndex = 16;
            lblClientes.Text = "label6";
            // 
            // lblTituloClientes
            // 
            lblTituloClientes.AutoSize = true;
            lblTituloClientes.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTituloClientes.ForeColor = Color.FromArgb(150, 150, 200);
            lblTituloClientes.Location = new Point(10, 10);
            lblTituloClientes.Name = "lblTituloClientes";
            lblTituloClientes.Size = new Size(61, 20);
            lblTituloClientes.TabIndex = 16;
            lblTituloClientes.Text = "Clientes";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(970, 20);
            label2.Name = "label2";
            label2.Size = new Size(205, 21);
            label2.TabIndex = 1;
            label2.Text = "PAINEL ADMINISTRATIVO";
            // 
            // lblSistema
            // 
            lblSistema.AutoSize = true;
            lblSistema.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSistema.ForeColor = SystemColors.ButtonHighlight;
            lblSistema.Location = new Point(20, 20);
            lblSistema.Name = "lblSistema";
            lblSistema.Size = new Size(171, 21);
            lblSistema.TabIndex = 0;
            lblSistema.Text = "SISTEMA COMERCIAL";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top;
            panel2.BackColor = Color.FromArgb(9, 3, 90);
            panel2.Controls.Add(lblSistema);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1194, 60);
            panel2.TabIndex = 9;
            panel2.Paint += panel2_Paint;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 23);
            ClientSize = new Size(1264, 761);
            Controls.Add(cardProdutos);
            Controls.Add(cardClientes);
            Controls.Add(cardVendas);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(dgvUltimasVendas);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MinimumSize = new Size(1024, 600);
            Name = "FormMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Comercial";
            WindowState = FormWindowState.Maximized;
            Load += FormMenu_Load;
            Paint += FormMenu_Paint;
            KeyDown += FormMenu_KeyDown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUltimasVendas).EndInit();
            cardVendas.ResumeLayout(false);
            cardVendas.PerformLayout();
            cardProdutos.ResumeLayout(false);
            cardProdutos.PerformLayout();
            cardClientes.ResumeLayout(false);
            cardClientes.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnProdutos;
        private Button btnVendas;
        private Button btnSair;
        private Button btnCaixa;
        private System.Windows.Forms.Timer timerFade;
        private Button btnFornecedor;
        private Button btnContasReceber;
        private Button btnContasPagar;
        private Panel panel1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private Panel panel3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private Button button1;
        private Label lblVendas;
        private Label lblProdutos;
        private DataGridView dgvUltimasVendas;
        private Panel cardVendas;
        private Panel cardClientes;
        private Panel cardProdutos;
        private Label label2;
        private Label lblSistema;
        private Panel panel2;
        private Label lblTituloVendas;
        private Label lblTituloProdutos;
        private Label lblClientes;
        private Label lblTituloClientes;
    }
}
