namespace SistemaComercial.UI;

public static class AppTheme
{
    // Paleta central do sistema.
    // A ideia e evitar telas com muitas variações de azul e usar cor com significado:
    // verde confirma, âmbar chama atenção financeira, vermelho alerta, roxo identifica cadastros.
    public static readonly Color Background = Color.FromArgb(10, 15, 28);
    public static readonly Color Header = Color.FromArgb(31, 28, 86);
    public static readonly Color Sidebar = Color.FromArgb(8, 13, 26);
    public static readonly Color MenuActive = Color.FromArgb(33, 43, 70);
    public static readonly Color MenuHover = Color.FromArgb(24, 34, 56);
    public static readonly Color Card = Color.FromArgb(22, 30, 48);
    public static readonly Color CardElevated = Color.FromArgb(30, 41, 64);
    public static readonly Color Text = Color.FromArgb(248, 250, 252);
    public static readonly Color TextMuted = Color.FromArgb(190, 202, 219);
    public static readonly Color Border = Color.FromArgb(61, 75, 103);
    public static readonly Color Primary = Color.FromArgb(59, 130, 246);
    public static readonly Color PrimaryDark = Color.FromArgb(29, 78, 216);
    public static readonly Color Success = Color.FromArgb(21, 128, 61);
    public static readonly Color SuccessDark = Color.FromArgb(20, 83, 45);
    public static readonly Color Warning = Color.FromArgb(180, 83, 9);
    public static readonly Color WarningDark = Color.FromArgb(120, 53, 15);
    public static readonly Color Danger = Color.FromArgb(190, 18, 60);
    public static readonly Color DangerDark = Color.FromArgb(136, 19, 55);
    public static readonly Color Teal = Color.FromArgb(15, 118, 110);
    public static readonly Color Purple = Color.FromArgb(109, 40, 217);
    public static readonly Color Input = Color.FromArgb(249, 250, 251);
    public static readonly Color InputText = Color.FromArgb(15, 23, 42);

    public const int HeaderHeight = 60;
    public const int Margin = 24;

    // Aplica o padrão visual usado nas telas internas: fundo, fonte, tamanho,
    // cabeçalho e estilos dos controles filhos.
    public static void ApplyForm(Form form, string title)
    {
        form.BackColor = Background;
        form.Font = new Font("Segoe UI", 9F);
        form.ForeColor = Text;
        form.FormBorderStyle = FormBorderStyle.Sizable;
        form.MaximizeBox = true;
        form.MinimumSize = new Size(900, 600);
        form.ClientSize = new Size(1100, 680);
        form.StartPosition = FormStartPosition.CenterScreen;
        form.Text = "Sistema Comercial - " + title;
        EnsureHeader(form, title);
        StyleChildren(form);
        CenterWhenReady(form);
    }

    public static void CenterWhenReady(Form form)
    {
        form.StartPosition = FormStartPosition.CenterScreen;
        form.Shown -= CenterFormOnShown;
        form.Shown += CenterFormOnShown;
    }

    private static void CenterFormOnShown(object? sender, EventArgs e)
    {
        if (sender is Form form)
        {
            Rectangle area = Screen.FromControl(form).WorkingArea;
            form.Location = new Point(
                area.Left + (area.Width - form.Width) / 2,
                area.Top + (area.Height - form.Height) / 2);
        }
    }

    public static Panel EnsureHeader(Form form, string title)
    {
        // Evita criar duas barras superiores caso o tema seja aplicado mais de uma vez.
        if (form.Controls["appHeader"] is Panel existingHeader)
        {
            return existingHeader;
        }

        var header = new Panel
        {
            Name = "appHeader",
            BackColor = Header,
            Dock = DockStyle.Top,
            Height = HeaderHeight
        };

        var systemLabel = new Label
        {
            AutoSize = true,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
            ForeColor = Text,
            Location = new Point(20, 19),
            Text = "SISTEMA COMERCIAL"
        };

        var screenLabel = new Label
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            AutoSize = true,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
            ForeColor = Text,
            Text = title.ToUpperInvariant()
        };

        header.Controls.Add(systemLabel);
        header.Controls.Add(screenLabel);
        header.Resize += (_, _) => screenLabel.Location = new Point(header.Width - screenLabel.Width - 20, 19);
        form.Controls.Add(header);
        header.BringToFront();
        screenLabel.Location = new Point(header.Width - screenLabel.Width - 20, 19);

        return header;
    }

    public static void StyleChildren(Control parent)
    {
        // Percorre a árvore de controles da tela e aplica o estilo correto para cada tipo.
        // Isso mantém todas as telas com aparência consistente sem repetir código.
        foreach (Control control in parent.Controls)
        {
            switch (control)
            {
                case Button button:
                    StyleButton(button);
                    break;
                case DataGridView grid:
                    StyleGrid(grid);
                    break;
                case TextBox textBox:
                    StyleInput(textBox);
                    break;
                case ComboBox comboBox:
                    StyleInput(comboBox);
                    break;
                case DateTimePicker dateTimePicker:
                    StyleInput(dateTimePicker);
                    break;
                case Label label when control.Parent?.Name != "appHeader":
                    StyleLabel(label);
                    break;
                case Panel panel when panel.Name != "appHeader":
                    panel.BackColor = Card;
                    break;
            }

            if (control.HasChildren)
            {
                StyleChildren(control);
            }
        }
    }

    public static void StyleButton(Button button)
    {
        button.BackColor = GetButtonColor(button);
        button.Cursor = Cursors.Hand;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = Lighten(button.BackColor, 20);
        button.FlatAppearance.MouseDownBackColor = Darken(button.BackColor, 18);
        button.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        button.ForeColor = Text;
        button.MinimumSize = new Size(120, 40);
        button.AutoEllipsis = true;
        button.ImageAlign = ContentAlignment.MiddleCenter;
        button.TextAlign = ContentAlignment.MiddleCenter;
        button.TextImageRelation = TextImageRelation.ImageBeforeText;
        button.UseVisualStyleBackColor = false;
    }

    public static void StyleLabel(Label label)
    {
        label.BackColor = Color.Transparent;
        label.ForeColor = Text;
        // Mantém títulos e indicadores grandes definidos pela própria tela.
        if (label.Font.Size <= 10F)
        {
            label.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
        }
    }

    public static void StyleInput(Control control)
    {
        control.BackColor = Input;
        control.ForeColor = InputText;
        control.Font = new Font("Segoe UI", 9.5F);

        if (control is ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
        }
    }

    public static void StyleGrid(DataGridView grid)
    {
        // Tabelas precisam de contraste alto porque são a parte mais consultada do ERP.
        // Aqui removemos estilos herdados do designer e deixamos linhas, seleção e cabeçalho previsíveis.
        grid.BackgroundColor = Background;
        grid.BorderStyle = BorderStyle.FixedSingle;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        grid.EnableHeadersVisualStyles = false;
        grid.GridColor = Border;
        grid.RowHeadersVisible = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AllowUserToResizeRows = false;
        grid.ColumnHeadersHeight = 38;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.RowTemplate.Height = 32;

        grid.ColumnHeadersDefaultCellStyle.BackColor = Header;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Text;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Header;
        grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Text;

        grid.RowsDefaultCellStyle.BackColor = Card;
        grid.RowsDefaultCellStyle.ForeColor = Text;
        grid.RowsDefaultCellStyle.SelectionBackColor = PrimaryDark;
        grid.RowsDefaultCellStyle.SelectionForeColor = Color.White;

        grid.DefaultCellStyle.BackColor = Card;
        grid.DefaultCellStyle.ForeColor = Text;
        grid.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
        grid.DefaultCellStyle.SelectionBackColor = PrimaryDark;
        grid.DefaultCellStyle.SelectionForeColor = Color.White;
        grid.AlternatingRowsDefaultCellStyle.BackColor = CardElevated;
        grid.AlternatingRowsDefaultCellStyle.ForeColor = Text;
        grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = PrimaryDark;
        grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
    }

    // Centraliza cards de cadastro mantendo margens mínimas em telas pequenas.
    public static void CenterCard(Form form, Panel card, int width, int height)
    {
        card.Size = new Size(Math.Min(width, form.ClientSize.Width - Margin * 2), height);
        card.Location = new Point(
            Math.Max(Margin, (form.ClientSize.Width - card.Width) / 2),
            Math.Max(HeaderHeight + Margin, (form.ClientSize.Height - card.Height + HeaderHeight) / 2));
    }

    public static void ApplyMenuTheme(Form form, Panel sidebar, Panel header, DataGridView grid, params Panel[] cards)
    {
        // O menu recebe tratamento próprio porque tem sidebar recolhível e cards de resumo.
        form.BackColor = Background;
        sidebar.BackColor = Sidebar;
        header.BackColor = Header;

        foreach (Control control in sidebar.Controls)
        {
            if (control is Button button)
            {
                StyleButton(button);
            }
        }

        StyleGrid(grid);

        foreach (Panel card in cards)
        {
            card.BackColor = Card;
        }

        CenterWhenReady(form);
    }

    public static Color GetDashboardCardColor(string name)
    {
        return name switch
        {
            "cardVendas" => SuccessDark,
            "cardProdutos" => Color.FromArgb(88, 52, 12),
            "cardClientes" => Color.FromArgb(76, 29, 149),
            _ => Card
        };
    }

    private static Color GetButtonColor(Button button)
    {
        // Escolha semântica de cor: o nome/texto do botão indica a intenção da ação.
        // Isso ajuda o usuário a reconhecer risco, sucesso e financeiro rapidamente.
        string name = button.Name.ToLowerInvariant();
        string text = button.Text.ToLowerInvariant();
        string semantic = name + " " + text;

        if (semantic.Contains("excluir") || semantic.Contains("cancelar") || semantic.Contains("remover") || semantic.Contains("sair"))
        {
            return Danger;
        }

        if (semantic.Contains("salvar") || semantic.Contains("entrar") || semantic.Contains("finalizar") || semantic.Contains("receber") || semantic.Contains("vender"))
        {
            return Success;
        }

        if (semantic.Contains("pagar") || semantic.Contains("caixa") || semantic.Contains("contas"))
        {
            return Warning;
        }

        if (semantic.Contains("cliente") || semantic.Contains("fornecedor"))
        {
            return Purple;
        }

        return Primary;
    }

    private static Color Lighten(Color color, int amount)
    {
        return Color.FromArgb(
            Math.Min(255, color.R + amount),
            Math.Min(255, color.G + amount),
            Math.Min(255, color.B + amount));
    }

    private static Color Darken(Color color, int amount)
    {
        return Color.FromArgb(
            Math.Max(0, color.R - amount),
            Math.Max(0, color.G - amount),
            Math.Max(0, color.B - amount));
    }
}
