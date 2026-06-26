namespace SistemaComercial.Data;

public static class BackupService
{
    public static string Criar(string pastaDestino)
    {
        Directory.CreateDirectory(pastaDestino);
        string destino = Path.Combine(
            pastaDestino,
            $"SistemaComercial_backup_{DateTime.Now:yyyyMMdd_HHmmss}.db");
        using var origem = Database.GetConnection();
        using var copia = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={destino}");
        origem.Open();
        copia.Open();
        origem.BackupDatabase(copia);
        return destino;
    }

    public static void Restaurar(string arquivoBackup)
    {
        if (!File.Exists(arquivoBackup))
        {
            throw new FileNotFoundException("Arquivo de backup não encontrado.", arquivoBackup);
        }

        string seguranca = Database.DatabasePath + $".antes_restauracao_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
        if (File.Exists(Database.DatabasePath))
        {
            File.Copy(Database.DatabasePath, seguranca, true);
        }

        File.Copy(arquivoBackup, Database.DatabasePath, true);
        Database.CriarTabela();
    }
}
