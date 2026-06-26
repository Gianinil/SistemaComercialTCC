using SistemaComercial.Data.Repositories;
using SistemaComercial.Models;
using SistemaComercial.UI;

namespace SistemaComercial;

public sealed class FormGerenciarClientes : Form
{
    private readonly DataGridView grid = new()
    {
        ReadOnly = true,
        MultiSelect = false,
        Dock = DockStyle.Fill,
        AutoGenerateColumns = false,
        AllowUserToAddRows = false
    };
    private readonly TextBox busca = new() { PlaceholderText = "Buscar por nome ou CPF..." };
    private readonly TextBox nome = new();
    private readonly TextBox cpf = new();
    private readonly TextBox telefone = new();
    private readonly TextBox email = new();
    private readonly TextBox endereco = new();
    private int? clienteId;

    public FormGerenciarClientes()
    {
        Text = "Clientes cadastrados";
        KeyPreview = true;
        ConfigurarGrid();
        BuildLayout();
        FormValidation.ApplyCpfMask(cpf);
        FormValidation.ApplyPhoneMask(telefone);
        busca.TextChanged += (_, _) => Carregar();
        grid.SelectionChanged += (_, _) => PreencherSelecionado();
        Load += (_, _) => { AppTheme.ApplyForm(this, "Clientes cadastrados"); Carregar(); };
        Activated += (_, _) => Carregar();
        KeyDown += (_, e) => { if (e.KeyCode == Keys.Escape) Close(); };
    }

    private void ConfigurarGrid()
    {
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Cliente.Nome), HeaderText = "Nome", FillWeight = 26 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Cliente.CpfCnpj), HeaderText = "CPF", FillWeight = 18 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Cliente.Telefone), HeaderText = "Telefone", FillWeight = 18 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Cliente.Email), HeaderText = "E-mail", FillWeight = 22 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Cliente.Endereco), HeaderText = "Endereço", FillWeight = 30 });
    }

    private void BuildLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24, 84, 24, 24),
            ColumnCount = 2,
            RowCount = 2,
            BackColor = AppTheme.Background
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        busca.Dock = DockStyle.Fill;
        root.Controls.Add(busca, 0, 0);
        root.SetColumnSpan(busca, 2);
        root.Controls.Add(grid, 0, 1);

        var editor = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20),
            RowCount = 13,
            ColumnCount = 1,
            BackColor = AppTheme.Card
        };
        editor.Controls.Add(Titulo("Editar cliente"));
        AddField(editor, "Nome *", nome);
        AddField(editor, "CPF *", cpf);
        AddField(editor, "Telefone *", telefone);
        AddField(editor, "E-mail (opcional)", email);
        AddField(editor, "Endereço (opcional)", endereco);

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
        var salvar = new Button { Name = "btnSalvar", Text = "Salvar alterações" };
        var novo = new Button { Name = "btnNovo", Text = "Novo cliente" };
        salvar.Click += (_, _) => Salvar();
        novo.Click += (_, _) => { using var form = new FormClientes(); form.ShowDialog(this); Carregar(); };
        buttons.Controls.Add(salvar);
        buttons.Controls.Add(novo);
        editor.Controls.Add(buttons);
        root.Controls.Add(editor, 1, 1);
        Controls.Add(root);

        busca.TabIndex = 0;
        grid.TabIndex = 1;
        nome.TabIndex = 2;
        cpf.TabIndex = 3;
        telefone.TabIndex = 4;
        email.TabIndex = 5;
        endereco.TabIndex = 6;
        salvar.TabIndex = 7;
        novo.TabIndex = 8;
    }

    private static Label Titulo(string text) => new()
    {
        Text = text,
        AutoSize = true,
        Font = new Font("Segoe UI", 15F, FontStyle.Bold),
        Margin = new Padding(0, 0, 0, 14)
    };

    private static void AddField(TableLayoutPanel panel, string label, Control input)
    {
        panel.Controls.Add(new Label { Text = label, AutoSize = true, Margin = new Padding(0, 8, 0, 4) });
        input.Dock = DockStyle.Top;
        panel.Controls.Add(input);
    }

    private void Carregar()
    {
        int? selecionado = clienteId;
        grid.DataSource = null;
        grid.DataSource = ClienteRepository.Listar(busca.Text.Trim());
        AppTheme.StyleGrid(grid);
        if (selecionado.HasValue)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.DataBoundItem is Cliente item && item.Id == selecionado.Value)
                {
                    row.Selected = true;
                    grid.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }
    }

    private void PreencherSelecionado()
    {
        if (grid.CurrentRow?.DataBoundItem is not Cliente cliente) return;
        clienteId = cliente.Id;
        nome.Text = cliente.Nome;
        cpf.Text = cliente.CpfCnpj;
        telefone.Text = cliente.Telefone;
        email.Text = cliente.Email;
        endereco.Text = cliente.Endereco;
    }

    private void Salvar()
    {
        if (!clienteId.HasValue) { MessageBox.Show("Selecione um cliente na tabela."); return; }
        if (string.IsNullOrWhiteSpace(nome.Text)) { MessageBox.Show("Informe o nome."); return; }
        if (!FormValidation.IsCpfValid(cpf.Text)) { MessageBox.Show("Informe um CPF válido."); return; }
        if (!FormValidation.IsPhoneValid(telefone.Text)) { MessageBox.Show("Informe um telefone válido."); return; }
        if (!string.IsNullOrWhiteSpace(email.Text) && !FormValidation.IsEmailValid(email.Text))
        { MessageBox.Show("O e-mail informado não é válido."); return; }

        ClienteRepository.Atualizar(new Cliente
        {
            Id = clienteId.Value,
            Nome = nome.Text.Trim(),
            CpfCnpj = FormValidation.OnlyDigits(cpf.Text),
            Telefone = FormValidation.OnlyDigits(telefone.Text),
            Email = email.Text.Trim(),
            Endereco = endereco.Text.Trim()
        });
        MessageBox.Show("Cliente atualizado com sucesso.");
        Carregar();
    }
}
