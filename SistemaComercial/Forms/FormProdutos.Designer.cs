namespace SistemaComercial.Forms
{
    partial class FormProdutos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProdutos));
            dgvProdutos = new DataGridView();
            panelBotoes = new Panel();
            Excluir = new Button();
            Editar = new Button();
            Novo = new Button();
            txtBuscar = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).BeginInit();
            panelBotoes.SuspendLayout();
            SuspendLayout();
            // 
            // dgvProdutos
            // 
            dgvProdutos.AllowUserToAddRows = false;
            dgvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProdutos.BackgroundColor = SystemColors.AppWorkspace;
            dgvProdutos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvProdutos.DefaultCellStyle = dataGridViewCellStyle1;
            dgvProdutos.Dock = DockStyle.Top;
            dgvProdutos.GridColor = Color.FromArgb(62, 62, 66);
            dgvProdutos.Location = new Point(0, 0);
            dgvProdutos.Name = "dgvProdutos";
            dgvProdutos.ReadOnly = true;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dgvProdutos.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dgvProdutos.Size = new Size(1233, 567);
            dgvProdutos.TabIndex = 0;
            dgvProdutos.CellContentClick += dataGridView1_CellContentClick;
            // 
            // panelBotoes
            // 
            panelBotoes.BackColor = SystemColors.Control;
            panelBotoes.Controls.Add(Excluir);
            panelBotoes.Controls.Add(Editar);
            panelBotoes.Controls.Add(Novo);
            panelBotoes.Dock = DockStyle.Bottom;
            panelBotoes.Location = new Point(0, 605);
            panelBotoes.Name = "panelBotoes";
            panelBotoes.Size = new Size(1233, 96);
            panelBotoes.TabIndex = 1;
            panelBotoes.Paint += panelBotoes_Paint;
            // 
            // Excluir
            // 
            Excluir.BackColor = Color.FromArgb(5, 1, 71);
            Excluir.Cursor = Cursors.Hand;
            Excluir.FlatAppearance.BorderSize = 0;
            Excluir.FlatStyle = FlatStyle.Flat;
            Excluir.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Excluir.ForeColor = Color.White;
            Excluir.Location = new Point(997, 23);
            Excluir.Name = "Excluir";
            Excluir.Size = new Size(140, 51);
            Excluir.TabIndex = 2;
            Excluir.Text = "EXCLUIR";
            Excluir.UseVisualStyleBackColor = false;
            Excluir.Click += btnExcluir_Click;
            // 
            // Editar
            // 
            Editar.BackColor = Color.FromArgb(5, 1, 71);
            Editar.Cursor = Cursors.Hand;
            Editar.FlatAppearance.BorderSize = 0;
            Editar.FlatStyle = FlatStyle.Flat;
            Editar.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Editar.ForeColor = Color.White;
            Editar.Location = new Point(543, 23);
            Editar.Name = "Editar";
            Editar.Size = new Size(140, 51);
            Editar.TabIndex = 1;
            Editar.Text = "EDITAR";
            Editar.UseVisualStyleBackColor = false;
            Editar.Click += Editar_Click;
            // 
            // Novo
            // 
            Novo.BackColor = Color.FromArgb(5, 1, 71);
            Novo.Cursor = Cursors.Hand;
            Novo.FlatAppearance.BorderSize = 0;
            Novo.FlatStyle = FlatStyle.Flat;
            Novo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Novo.ForeColor = Color.White;
            Novo.Location = new Point(109, 23);
            Novo.Name = "Novo";
            Novo.Size = new Size(140, 51);
            Novo.TabIndex = 0;
            Novo.Text = "NOVO";
            Novo.UseVisualStyleBackColor = false;
            Novo.Click += btnNovo_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(997, 576);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(186, 23);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(889, 577);
            label1.Name = "label1";
            label1.Size = new Size(109, 17);
            label1.TabIndex = 3;
            label1.Text = "Buscar produto: ";
            // 
            // FormProdutos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1233, 701);
            Controls.Add(label1);
            Controls.Add(txtBuscar);
            Controls.Add(panelBotoes);
            Controls.Add(dgvProdutos);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            Name = "FormProdutos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Comercial";
            Load += FormProdutos_Load;
            KeyDown += FormProdutos_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgvProdutos).EndInit();
            panelBotoes.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvProdutos;
        private Panel panelBotoes;
        private Button Excluir;
        private Button Editar;
        private Button Novo;
        private TextBox txtBuscar;
        private Label label1;
    }
}