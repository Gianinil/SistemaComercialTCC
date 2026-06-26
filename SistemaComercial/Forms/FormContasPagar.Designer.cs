namespace SistemaComercial
{
    partial class FormContasPagar
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
            label1 = new Label();
            txtValor = new TextBox();
            label2 = new Label();
            label3 = new Label();
            dtData = new DateTimePicker();
            cbStatus = new ComboBox();
            label4 = new Label();
            btnSalvar = new Button();
            dgvPagar = new DataGridView();
            btnPagar = new Button();
            cbFornecedor = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvPagar).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(60, 548);
            label1.Name = "label1";
            label1.Size = new Size(73, 15);
            label1.TabIndex = 0;
            label1.Text = "Fornecedor: ";
            // 
            // txtValor
            // 
            txtValor.Location = new Point(60, 628);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(214, 23);
            txtValor.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(60, 610);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 2;
            label2.Text = "Valor: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(361, 548);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 4;
            label3.Text = "Valor: ";
            // 
            // dtData
            // 
            dtData.Format = DateTimePickerFormat.Custom;
            dtData.Location = new Point(361, 566);
            dtData.Name = "dtData";
            dtData.Size = new Size(200, 23);
            dtData.TabIndex = 5;
            // 
            // cbStatus
            // 
            cbStatus.FormattingEnabled = true;
            cbStatus.Location = new Point(361, 628);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(121, 23);
            cbStatus.TabIndex = 6;
            cbStatus.SelectedIndexChanged += cbStatus_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(361, 610);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 7;
            label4.Text = "Status: ";
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.FromArgb(5, 1, 71);
            btnSalvar.Cursor = Cursors.Hand;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnSalvar.ForeColor = SystemColors.Control;
            btnSalvar.Location = new Point(658, 552);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(114, 41);
            btnSalvar.TabIndex = 8;
            btnSalvar.Text = "SALVAR";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // dgvPagar
            // 
            dgvPagar.AllowUserToAddRows = false;
            dgvPagar.AllowUserToDeleteRows = false;
            dgvPagar.AllowUserToResizeColumns = false;
            dgvPagar.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvPagar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvPagar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPagar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPagar.Dock = DockStyle.Top;
            dgvPagar.Location = new Point(0, 0);
            dgvPagar.MultiSelect = false;
            dgvPagar.Name = "dgvPagar";
            dgvPagar.ReadOnly = true;
            dgvPagar.RightToLeft = RightToLeft.No;
            dgvPagar.RowHeadersVisible = false;
            dgvPagar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPagar.Size = new Size(1269, 542);
            dgvPagar.TabIndex = 9;
            dgvPagar.CellContentClick += dgvPagar_CellContentClick;
            // 
            // btnPagar
            // 
            btnPagar.BackColor = Color.FromArgb(5, 1, 71);
            btnPagar.Cursor = Cursors.Hand;
            btnPagar.FlatStyle = FlatStyle.Flat;
            btnPagar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnPagar.ForeColor = SystemColors.Control;
            btnPagar.Location = new Point(1144, 549);
            btnPagar.Name = "btnPagar";
            btnPagar.Size = new Size(114, 41);
            btnPagar.TabIndex = 10;
            btnPagar.Text = "PAGAR";
            btnPagar.UseVisualStyleBackColor = false;
            btnPagar.Click += btnPagar_Click;
            // 
            // cbFornecedor
            // 
            cbFornecedor.FormattingEnabled = true;
            cbFornecedor.Location = new Point(60, 566);
            cbFornecedor.Name = "cbFornecedor";
            cbFornecedor.Size = new Size(121, 23);
            cbFornecedor.TabIndex = 11;
            // 
            // FormContasPagar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1269, 663);
            Controls.Add(cbFornecedor);
            Controls.Add(btnPagar);
            Controls.Add(dgvPagar);
            Controls.Add(btnSalvar);
            Controls.Add(label4);
            Controls.Add(cbStatus);
            Controls.Add(dtData);
            Controls.Add(label3);
            Controls.Add(txtValor);
            Controls.Add(label2);
            Controls.Add(label1);
            KeyPreview = true;
            Name = "FormContasPagar";
            Text = "FormContasPagar";
            Load += FormContasPagar_Load;
            KeyDown += FormContasPagar_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgvPagar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtValor;
        private Label label2;
        private Label label3;
        private DateTimePicker dtData;
        private ComboBox cbStatus;
        private Label label4;
        private Button btnSalvar;
        private DataGridView dgvPagar;
        private Button btnPagar;
        private ComboBox cbFornecedor;
    }
}