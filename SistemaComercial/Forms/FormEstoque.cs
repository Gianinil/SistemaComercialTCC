using SistemaComercial.Data;
using SistemaComercial.UI;

namespace SistemaComercial;

public sealed class FormEstoque : Form
{
    private readonly TextBox busca = new() { PlaceholderText = "Buscar produto no estoque..." };
    private readonly Label produtos = CardValue();
    private readonly Label unidades = CardValue();
    private readonly Label baixos = CardValue();
    private readonly Label valor = CardValue();
    private readonly DataGridView posicao = new() { ReadOnly = true, Dock = DockStyle.Fill };
    private readonly DataGridView movimentos = new() { ReadOnly = true, Dock = DockStyle.Fill };

    public FormEstoque()
    {
        KeyPreview = true;
        BuildLayout();
        busca.TextChanged += (_, _) => Carregar();
        Load += (_, _) => { AppTheme.ApplyForm(this, "Estoque integrado"); Carregar(); };
        KeyDown += (_, e) => { if (e.KeyCode == Keys.Escape) Close(); };
    }

    private void BuildLayout()
    {
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24, 84, 24, 24),
            RowCount = 3,
            ColumnCount = 1,
            BackColor = AppTheme.Background
        };
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 96));
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var cards = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4 };
        for (int i = 0; i < 4; i++) cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        cards.Controls.Add(Card("Produtos cadastrados", produtos), 0, 0);
        cards.Controls.Add(Card("Unidades disponíveis", unidades), 1, 0);
        cards.Controls.Add(Card("Atenção no estoque", baixos), 2, 0);
        cards.Controls.Add(Card("Valor potencial", valor), 3, 0);
        root.Controls.Add(cards, 0, 0);
        busca.Dock = DockStyle.Fill;
        root.Controls.Add(busca, 0, 1);
        var tabs = new TabControl { Dock = DockStyle.Fill };
        var tabPosicao = new TabPage("Posição atual");
        var tabMovimentos = new TabPage("Histórico de entradas e saídas");
        tabPosicao.Controls.Add(posicao);
        tabMovimentos.Controls.Add(movimentos);
        tabs.TabPages.Add(tabPosicao);
        tabs.TabPages.Add(tabMovimentos);
        root.Controls.Add(tabs, 0, 2);
        Controls.Add(root);

        busca.TabIndex = 0;
        tabs.TabIndex = 1;
        posicao.TabIndex = 2;
        movimentos.TabIndex = 3;
    }

    private static Label CardValue() => new()
    {
        AutoSize = true,
        Font = new Font("Segoe UI", 18F, FontStyle.Bold),
        ForeColor = AppTheme.Text
    };

    private static Panel Card(string title, Label valueLabel)
    {
        var panel = new Panel { Dock = DockStyle.Fill, BackColor = AppTheme.Card, Margin = new Padding(6) };
        panel.Controls.Add(new Label { Text = title, AutoSize = true, Location = new Point(14, 12), ForeColor = AppTheme.TextMuted });
        valueLabel.Location = new Point(14, 42);
        panel.Controls.Add(valueLabel);
        return panel;
    }

    private void Carregar()
    {
        EstoqueResumo resumo = EstoqueService.ObterResumo(busca.Text);
        produtos.Text = resumo.Produtos.ToString();
        unidades.Text = resumo.Unidades.ToString();
        baixos.Text = resumo.EstoqueBaixo.ToString();
        baixos.ForeColor = resumo.EstoqueBaixo > 0 ? AppTheme.Warning : AppTheme.Success;
        valor.Text = resumo.ValorEstoque.ToString("C2");
        posicao.DataSource = resumo.Posicao;
        movimentos.DataSource = resumo.Movimentacoes;
        AppTheme.StyleGrid(posicao);
        AppTheme.StyleGrid(movimentos);
        if (posicao.Columns["PrecoVenda"] != null) posicao.Columns["PrecoVenda"].DefaultCellStyle.Format = "C2";
        posicao.CellFormatting -= FormatarSituacao;
        posicao.CellFormatting += FormatarSituacao;
    }

    private void FormatarSituacao(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (posicao.Columns[e.ColumnIndex].Name != "Situacao" || e.Value == null) return;
        string situacao = e.Value.ToString() ?? "";
        e.CellStyle.ForeColor = situacao == "Normal" ? AppTheme.Success : AppTheme.Warning;
        e.CellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
    }
}
