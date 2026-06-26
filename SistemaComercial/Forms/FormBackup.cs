using SistemaComercial.Data;
using SistemaComercial.UI;

namespace SistemaComercial;

public sealed class FormBackup : Form
{
    private readonly Label caminho = new()
    {
        AutoSize = false,
        Dock = DockStyle.Fill,
        TextAlign = ContentAlignment.MiddleLeft
    };

    public FormBackup()
    {
        KeyPreview = true;
        BuildLayout();
        Load += (_, _) => AppTheme.ApplyForm(this, "Backup do banco");
        KeyDown += (_, e) => { if (e.KeyCode == Keys.Escape) Close(); };
    }

    private void BuildLayout()
    {
        var card = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(60, 120, 60, 60),
            RowCount = 5,
            ColumnCount = 1,
            BackColor = AppTheme.Background
        };
        card.Controls.Add(new Label
        {
            Text = "Proteção dos dados",
            AutoSize = true,
            Font = new Font("Segoe UI", 22F, FontStyle.Bold)
        });
        card.Controls.Add(new Label
        {
            Text = "Crie uma cópia do banco ou restaure uma cópia anterior. Antes de restaurar, o sistema guarda automaticamente uma cópia de segurança do banco atual.",
            AutoSize = true,
            MaximumSize = new Size(760, 0),
            ForeColor = AppTheme.TextMuted
        });
        caminho.Text = "Banco atual: " + Database.DatabasePath;
        card.Controls.Add(caminho);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill };
        var criar = new Button { Name = "btnCriarBackup", Text = "Criar backup" };
        var restaurar = new Button { Name = "btnRestaurar", Text = "Restaurar backup" };
        criar.Click += (_, _) => Criar();
        restaurar.Click += (_, _) => Restaurar();
        actions.Controls.Add(criar);
        actions.Controls.Add(restaurar);
        card.Controls.Add(actions);
        Controls.Add(card);

        criar.TabIndex = 0;
        restaurar.TabIndex = 1;
    }

    private void Criar()
    {
        using var dialog = new FolderBrowserDialog { Description = "Escolha onde salvar o backup" };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        string arquivo = BackupService.Criar(dialog.SelectedPath);
        MessageBox.Show("Backup criado em:\n" + arquivo);
    }

    private void Restaurar()
    {
        using var dialog = new OpenFileDialog { Filter = "Backup SQLite (*.db)|*.db|Todos os arquivos (*.*)|*.*" };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        if (MessageBox.Show("Restaurar este backup substituirá os dados atuais. Continuar?", "Confirmação",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        BackupService.Restaurar(dialog.FileName);
        MessageBox.Show("Backup restaurado com sucesso. Reinicie o sistema.");
    }
}
