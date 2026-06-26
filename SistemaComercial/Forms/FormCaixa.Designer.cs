namespace SistemaComercial
{
    partial class FormCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCaixa));
            dgvCaixa = new DataGridView();
            lblEntradas = new Label();
            lblSaidas = new Label();
            lblSaldo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCaixa).BeginInit();
            SuspendLayout();
            // 
            // dgvCaixa
            // 
            dgvCaixa.AllowUserToAddRows = false;
            dgvCaixa.AllowUserToDeleteRows = false;
            dgvCaixa.AllowUserToResizeColumns = false;
            dgvCaixa.AllowUserToResizeRows = false;
            dgvCaixa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCaixa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCaixa.Dock = DockStyle.Top;
            dgvCaixa.Location = new Point(0, 0);
            dgvCaixa.Name = "dgvCaixa";
            dgvCaixa.ReadOnly = true;
            dgvCaixa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCaixa.Size = new Size(1233, 576);
            dgvCaixa.TabIndex = 0;
            dgvCaixa.CellContentClick += dgvCaixa_CellContentClick;
            // 
            // lblEntradas
            // 
            lblEntradas.AutoSize = true;
            lblEntradas.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblEntradas.ForeColor = SystemColors.ControlText;
            lblEntradas.Location = new Point(349, 634);
            lblEntradas.Name = "lblEntradas";
            lblEntradas.Size = new Size(111, 21);
            lblEntradas.TabIndex = 1;
            lblEntradas.Text = "Total Entradas";
            lblEntradas.Click += label1_Click;
            // 
            // lblSaidas
            // 
            lblSaidas.AutoSize = true;
            lblSaidas.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblSaidas.ForeColor = SystemColors.ControlText;
            lblSaidas.Location = new Point(557, 634);
            lblSaidas.Name = "lblSaidas";
            lblSaidas.Size = new Size(95, 21);
            lblSaidas.TabIndex = 2;
            lblSaidas.Text = "Total Saídas";
            // 
            // lblSaldo
            // 
            lblSaldo.AutoSize = true;
            lblSaldo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblSaldo.ForeColor = SystemColors.ControlText;
            lblSaldo.Location = new Point(757, 634);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(88, 21);
            lblSaldo.TabIndex = 3;
            lblSaldo.Text = "Saldo Final";
            // 
            // FormCaixa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1233, 701);
            Controls.Add(lblSaldo);
            Controls.Add(lblSaidas);
            Controls.Add(lblEntradas);
            Controls.Add(dgvCaixa);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            Name = "FormCaixa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Comercial";
            Load += FormCaixa_Load;
            KeyDown += FormCaixa_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgvCaixa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvCaixa;
        private Label lblEntradas;
        private Label lblSaidas;
        private Label lblSaldo;
    }
}