using SistemaComercial.Data;
using SistemaComercial.Data.Repositories;
using SistemaComercial.Models;
using SistemaComercial.UI;

namespace SistemaComercial;

public sealed class FormCompras : Form
{
    private readonly ComboBox fornecedor = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox produto = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly TextBox quantidade = new();
    private readonly TextBox custo = new();
    private readonly DateTimePicker vencimento = new() { Format = DateTimePickerFormat.Short };
    private readonly ComboBox metodo = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox status = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly TextBox observacao = new() { Multiline = true, Height = 56 };
    private readonly DataGridView grid = new() { ReadOnly = true, MultiSelect = false, Dock = DockStyle.Fill };
    private readonly Label total = new() { AutoSize = true, Font = new Font("Segoe UI", 18F, FontStyle.Bold) };
    private readonly List<ItemCompra> itens = new();

    public FormCompras()
    {
        KeyPreview = true;
        BuildLayout();
        metodo.Items.AddRange(new object[] { "Dinheiro", "Pix" });
        status.Items.AddRange(new object[] { "Pendente", "Pago" });
        metodo.SelectedIndex = 0;
        status.SelectedIndex = 0;
        vencimento.Value = DateTime.Today.AddDays(30);
        Load += (_, _) => { AppTheme.ApplyForm(this, "Compras de fornecedores"); CarregarListas(); AtualizarCarrinho(); };
        KeyDown += (_, e) => { if (e.KeyCode == Keys.Escape) Close(); };
    }

    private void BuildLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24, 84, 24, 24),
            ColumnCount = 2,
            RowCount = 1,
            BackColor = AppTheme.Background
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 330));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        var form = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20),
            BackColor = AppTheme.Card,
            ColumnCount = 1,
            AutoScroll = true
        };
        form.Controls.Add(Heading("Nova compra"));
        AddField(form, "Fornecedor *", fornecedor);
        AddField(form, "Produto *", produto);
        AddField(form, "Quantidade *", quantidade);
        AddField(form, "Custo unitário *", custo);
        var adicionar = new Button { Name = "btnAdicionar", Text = "Adicionar produto" };
        adicionar.Click += (_, _) => AdicionarItem();
        form.Controls.Add(adicionar);
        AddField(form, "Vencimento", vencimento);
        AddField(form, "Forma de pagamento", metodo);
        AddField(form, "Situação", status);
        AddField(form, "Observação", observacao);
        var finalizar = new Button { Name = "btnFinalizarCompra", Text = "Finalizar compra" };
        finalizar.Click += (_, _) => Finalizar();
        form.Controls.Add(finalizar);

        var content = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(16, 0, 0, 0) };
        content.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        content.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        content.Controls.Add(grid, 0, 0);
        content.Controls.Add(total, 0, 1);
        var remover = new Button { Name = "btnRemover", Text = "Remover selecionado", Dock = DockStyle.Left };
        remover.Click += (_, _) => RemoverItem();
        content.Controls.Add(remover, 0, 2);

        root.Controls.Add(form, 0, 0);
        root.Controls.Add(content, 1, 0);
        Controls.Add(root);

        fornecedor.TabIndex = 0;
        produto.TabIndex = 1;
        quantidade.TabIndex = 2;
        custo.TabIndex = 3;
        adicionar.TabIndex = 4;
        vencimento.TabIndex = 5;
        metodo.TabIndex = 6;
        status.TabIndex = 7;
        observacao.TabIndex = 8;
        finalizar.TabIndex = 9;
        grid.TabIndex = 10;
        remover.TabIndex = 11;
    }

    private static Label Heading(string text) => new()
    {
        Text = text,
        AutoSize = true,
        Font = new Font("Segoe UI", 15F, FontStyle.Bold),
        Margin = new Padding(0, 0, 0, 12)
    };

    private static void AddField(TableLayoutPanel panel, string label, Control input)
    {
        panel.Controls.Add(new Label { Text = label, AutoSize = true, Margin = new Padding(0, 8, 0, 4) });
        input.Dock = DockStyle.Top;
        panel.Controls.Add(input);
    }

    private void CarregarListas()
    {
        fornecedor.DataSource = FornecedorRepository.Listar();
        fornecedor.DisplayMember = nameof(Fornecedor.Nome);
        fornecedor.ValueMember = nameof(Fornecedor.Id);
        produto.DataSource = ProdutoRepository.Listar();
        produto.DisplayMember = nameof(Produto.Nome);
        produto.ValueMember = nameof(Produto.Id);
        fornecedor.SelectedIndex = -1;
        produto.SelectedIndex = -1;
    }

    private void AdicionarItem()
    {
        if (produto.SelectedItem is not Produto selecionado) { MessageBox.Show("Selecione um produto."); return; }
        if (!int.TryParse(quantidade.Text, out int qtd) || qtd <= 0) { MessageBox.Show("Informe uma quantidade válida."); return; }
        if (!decimal.TryParse(custo.Text, out decimal valor) || valor <= 0) { MessageBox.Show("Informe um custo válido."); return; }

        ItemCompra? existente = itens.FirstOrDefault(item => item.ProdutoId == selecionado.Id && item.CustoUnitario == valor);
        if (existente == null)
        {
            itens.Add(new ItemCompra { ProdutoId = selecionado.Id, ProdutoNome = selecionado.Nome, Quantidade = qtd, CustoUnitario = valor });
        }
        else
        {
            existente.Quantidade += qtd;
        }

        quantidade.Clear();
        custo.Clear();
        produto.SelectedIndex = -1;
        AtualizarCarrinho();
    }

    private void RemoverItem()
    {
        if (grid.CurrentRow == null || grid.CurrentRow.Index >= itens.Count) return;
        itens.RemoveAt(grid.CurrentRow.Index);
        AtualizarCarrinho();
    }

    private void AtualizarCarrinho()
    {
        grid.DataSource = null;
        grid.DataSource = itens.Select(item => new
        {
            Produto = item.ProdutoNome,
            item.Quantidade,
            CustoUnitario = item.CustoUnitario,
            item.Total
        }).ToList();
        AppTheme.StyleGrid(grid);
        if (grid.Columns["CustoUnitario"] != null) grid.Columns["CustoUnitario"].DefaultCellStyle.Format = "C2";
        if (grid.Columns["Total"] != null) grid.Columns["Total"].DefaultCellStyle.Format = "C2";
        total.Text = $"Total da compra: {itens.Sum(item => item.Total):C2}";
    }

    private void Finalizar()
    {
        if (fornecedor.SelectedItem is not Fornecedor selecionado) { MessageBox.Show("Selecione o fornecedor."); return; }
        try
        {
            int id = CompraService.Registrar(
                selecionado.Id,
                selecionado.Nome,
                vencimento.Value,
                metodo.Text,
                status.Text == "Pago",
                observacao.Text.Trim(),
                itens);
            MessageBox.Show($"Compra nº {id} registrada.\nO estoque e o contas a pagar foram atualizados.");
            itens.Clear();
            observacao.Clear();
            AtualizarCarrinho();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Não foi possível registrar a compra");
        }
    }
}
