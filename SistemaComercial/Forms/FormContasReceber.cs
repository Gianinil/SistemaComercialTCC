using Microsoft.Data.Sqlite;
using SistemaComercial.Data;
using SistemaComercial.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SistemaComercial
{
    public partial class FormContasReceber : Form
    {
        public FormContasReceber()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void dgvReceber_Load(object sender, EventArgs e)
        {
            // Prepara visual, status padr?o e lista de contas a receber.
            AplicarLayout();
            cbStatus.Items.Add("Pendente");
            cbStatus.Items.Add("Pago");
            cbStatus.SelectedIndex = 0;

            Carregar();
        }

        private void Carregar()
        {
            // Carrega todas as contas a receber na tabela.
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "SELECT * FROM ContasReceber";

                var cmd = new SqliteCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dgvReceber.DataSource = dt;
            }
        }



        private void dgvReceber_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Registra uma nova conta a receber.
            try
            {
                if (string.IsNullOrEmpty(txtCliente.Text))
                {
                    MessageBox.Show("Informe o cliente.");
                    return;
                }

                if (!decimal.TryParse(txtValor.Text, out decimal valor))
                {
                    MessageBox.Show("Valor inválido.");
                    return;
                }

                if (cbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Selecione o status.");
                    return;
                }

                string status = cbStatus.Text;
                string data = dtData.Value.ToString("yyyy-MM-dd");

                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string sql = @"INSERT INTO ContasReceber 
                (Cliente, Valor, Data, Status)
                VALUES (@c, @v, @d, @s)";

                    var cmd = new SqliteCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@c", txtCliente.Text);
                    cmd.Parameters.AddWithValue("@v", valor);
                    cmd.Parameters.AddWithValue("@d", data);
                    cmd.Parameters.AddWithValue("@s", status);
                    cmd.ExecuteNonQuery();

                    if (status == "Pago")
                    {
                        string sqlCaixa = @"INSERT INTO Caixa
                            (Tipo, Valor, Descricao, DataMovimento, MetodoPagamento, Cliente)
                            VALUES (@tipo, @valor, @desc, @data, @metodo, @cliente)";

                        var cmdCaixa = new SqliteCommand(sqlCaixa, conn);
                        cmdCaixa.Parameters.AddWithValue("@tipo", "Entrada");
                        cmdCaixa.Parameters.AddWithValue("@valor", valor);
                        cmdCaixa.Parameters.AddWithValue("@desc", "Recebimento - " + txtCliente.Text);
                        cmdCaixa.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmdCaixa.Parameters.AddWithValue("@metodo", "Manual");
                        cmdCaixa.Parameters.AddWithValue("@cliente", txtCliente.Text);
                        cmdCaixa.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Conta registrada!");
                Carregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnReceber_Click_1(object sender, EventArgs e)
        {
            // Recebe a conta selecionada, lan?a entrada no caixa e remove da lista pendente.
            try
            {
                if (dgvReceber.CurrentRow == null)
                {
                    MessageBox.Show("Selecione uma conta.");
                    return;
                }

                int id = Convert.ToInt32(dgvReceber.CurrentRow.Cells["Id"].Value);
                decimal valor = Convert.ToDecimal(dgvReceber.CurrentRow.Cells["Valor"].Value);
                string cliente = dgvReceber.CurrentRow.Cells["Cliente"].Value.ToString();

                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    //  ENTRADA NO CAIXA
                    string sqlCaixa = @"INSERT INTO Caixa 
            (Tipo, Valor, Descricao, DataMovimento, MetodoPagamento, Cliente)
            VALUES (@tipo, @valor, @desc, @data, @metodo, @cliente)";

                    var cmdCaixa = new SqliteCommand(sqlCaixa, conn);
                    cmdCaixa.Parameters.AddWithValue("@tipo", "Entrada");
                    cmdCaixa.Parameters.AddWithValue("@valor", valor);
                    cmdCaixa.Parameters.AddWithValue("@desc", "Recebimento - " + cliente);
                    cmdCaixa.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmdCaixa.Parameters.AddWithValue("@metodo", "Manual");
                    cmdCaixa.Parameters.AddWithValue("@cliente", cliente);
                    cmdCaixa.ExecuteNonQuery();

                    //  REMOVE DA LISTA
                    string sqlDelete = "DELETE FROM ContasReceber WHERE Id = @id";
                    var cmdDelete = new SqliteCommand(sqlDelete, conn);
                    cmdDelete.Parameters.AddWithValue("@id", id);
                    cmdDelete.ExecuteNonQuery();
                }

                MessageBox.Show("Pagamento recebido!");
                Carregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void FormContasReceber_KeyDown(object sender, KeyEventArgs e)
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

        private void AplicarLayout()
        {
            AppTheme.ApplyForm(this, "Contas a Receber");
            AjustarLayout();
        }

        private void AjustarLayout()
        {
            // Responsividade da tela: tabela em cima e formul?rio financeiro no rodap?.
            int margem = AppTheme.Margin;
            int topoConteudo = AppTheme.HeaderHeight + margem;
            int alturaFormulario = 132;

            dgvReceber.Dock = DockStyle.None;
            dgvReceber.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReceber.Location = new Point(margem, topoConteudo);
            dgvReceber.Size = new Size(ClientSize.Width - margem * 2, ClientSize.Height - topoConteudo - alturaFormulario - margem);

            int formTop = dgvReceber.Bottom + 18;
            int col1 = margem + 12;
            int col2 = col1 + 300;
            int col3 = col2 + 300;

            label1.Location = new Point(col1, formTop);
            txtCliente.Location = new Point(col1, formTop + 22);
            txtCliente.Size = new Size(220, 26);

            label2.Location = new Point(col1, formTop + 58);
            txtValor.Location = new Point(col1, formTop + 80);
            txtValor.Size = new Size(220, 26);

            label3.Location = new Point(col2, formTop);
            dtData.Location = new Point(col2, formTop + 22);
            dtData.Size = new Size(220, 26);

            label4.Location = new Point(col2, formTop + 58);
            cbStatus.Location = new Point(col2, formTop + 80);
            cbStatus.Size = new Size(220, 26);

            btnSalvar.Location = new Point(col3, formTop + 22);
            btnSalvar.Size = new Size(140, 42);

            btnReceber.Location = new Point(ClientSize.Width - margem - 152, formTop + 22);
            btnReceber.Size = new Size(140, 42);
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
