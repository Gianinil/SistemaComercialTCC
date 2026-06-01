namespace SistemaComercial
{
    partial class FormContasReceber
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
            txtCliente = new TextBox();
            txtValor = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            dtData = new DateTimePicker();
            label4 = new Label();
            cbStatus = new ComboBox();
            btnSalvar = new Button();
            dgvReceber = new DataGridView();
            btnReceber = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReceber).BeginInit();
            SuspendLayout();
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(50, 544);
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(207, 23);
            txtCliente.TabIndex = 0;
            // 
            // txtValor
            // 
            txtValor.Location = new Point(50, 608);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(207, 23);
            txtValor.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 526);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 2;
            label1.Text = "Cliente: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 590);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 3;
            label2.Text = "Valor: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(322, 526);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 4;
            label3.Text = "Data: ";
            // 
            // dtData
            // 
            dtData.Format = DateTimePickerFormat.Custom;
            dtData.Location = new Point(322, 544);
            dtData.Name = "dtData";
            dtData.Size = new Size(200, 23);
            dtData.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(322, 590);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 6;
            label4.Text = "Status: ";
            // 
            // cbStatus
            // 
            cbStatus.FormattingEnabled = true;
            cbStatus.Location = new Point(322, 608);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(121, 23);
            cbStatus.TabIndex = 7;
            // 
            // btnSalvar
            // 
            btnSalvar.BackColor = Color.FromArgb(5, 1, 71);
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnSalvar.ForeColor = SystemColors.Control;
            btnSalvar.Location = new Point(603, 531);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(114, 41);
            btnSalvar.TabIndex = 8;
            btnSalvar.Text = "SALVAR";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.Click += btnSalvar_Click;
            // 
            // dgvReceber
            // 
            dgvReceber.AllowUserToAddRows = false;
            dgvReceber.AllowUserToDeleteRows = false;
            dgvReceber.AllowUserToResizeColumns = false;
            dgvReceber.AllowUserToResizeRows = false;
            dgvReceber.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReceber.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReceber.Dock = DockStyle.Top;
            dgvReceber.Location = new Point(0, 0);
            dgvReceber.Name = "dgvReceber";
            dgvReceber.ReadOnly = true;
            dgvReceber.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReceber.Size = new Size(1166, 518);
            dgvReceber.TabIndex = 9;
            dgvReceber.CellContentClick += dgvReceber_CellContentClick;
            // 
            // btnReceber
            // 
            btnReceber.BackColor = Color.FromArgb(5, 1, 71);
            btnReceber.FlatStyle = FlatStyle.Flat;
            btnReceber.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnReceber.ForeColor = SystemColors.Control;
            btnReceber.Location = new Point(1041, 529);
            btnReceber.Name = "btnReceber";
            btnReceber.Size = new Size(114, 41);
            btnReceber.TabIndex = 10;
            btnReceber.Text = "RECEBIDO";
            btnReceber.UseVisualStyleBackColor = false;
            btnReceber.Click += btnReceber_Click_1;
            // 
            // FormContasReceber
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1166, 647);
            Controls.Add(btnReceber);
            Controls.Add(dgvReceber);
            Controls.Add(btnSalvar);
            Controls.Add(cbStatus);
            Controls.Add(label4);
            Controls.Add(dtData);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtValor);
            Controls.Add(txtCliente);
            KeyPreview = true;
            MinimizeBox = false;
            Name = "FormContasReceber";
            Text = "FormContasReceber";
            Load += dgvReceber_Load;
            KeyDown += FormContasReceber_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgvReceber).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCliente;
        private TextBox txtValor;
        private Label label1;
        private Label label2;
        private Label label3;
        private DateTimePicker dtData;
        private Label label4;
        private ComboBox cbStatus;
        private Button btnSalvar;
        private DataGridView dgvReceber;
        private Button btnReceber;
    }
}