using Microsoft.Data.Sqlite;
using System.Data;

namespace SistemaComercial.Data;

public static class CaixaService
{
    public static int? ObterSessaoAberta(
        SqliteConnection? connection = null,
        SqliteTransaction? transaction = null)
    {
        bool ownsConnection = connection == null;
        connection ??= Database.GetConnection();
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        using var cmd = new SqliteCommand(
            "SELECT Id FROM CaixaSessoes WHERE Status = 'Aberto' ORDER BY Id DESC LIMIT 1",
            connection,
            transaction);
        object? result = cmd.ExecuteScalar();

        if (ownsConnection)
        {
            connection.Dispose();
        }

        return result == null || result == DBNull.Value ? null : Convert.ToInt32(result);
    }

    public static void Abrir(decimal valorInicial, string observacao)
    {
        using var conn = Database.GetConnection();
        conn.Open();
        if (ObterSessaoAberta(conn).HasValue)
        {
            throw new InvalidOperationException("Já existe um caixa aberto.");
        }

        const string sql = @"
            INSERT INTO CaixaSessoes (DataAbertura, ValorInicial, Observacao, Status)
            VALUES (@data, @valor, @observacao, 'Aberto')";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        cmd.Parameters.AddWithValue("@valor", valorInicial);
        cmd.Parameters.AddWithValue("@observacao", observacao);
        cmd.ExecuteNonQuery();
    }

    public static decimal CalcularSaldoSessao(int sessaoId, SqliteConnection? connection = null)
    {
        bool ownsConnection = connection == null;
        connection ??= Database.GetConnection();
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        const string sql = @"
            SELECT s.ValorInicial +
                   IFNULL(SUM(CASE WHEN c.Tipo = 'Entrada' THEN c.Valor ELSE -c.Valor END), 0)
              FROM CaixaSessoes s
              LEFT JOIN Caixa c ON c.SessaoId = s.Id
             WHERE s.Id = @id
          GROUP BY s.Id, s.ValorInicial";
        using var cmd = new SqliteCommand(sql, connection);
        cmd.Parameters.AddWithValue("@id", sessaoId);
        decimal saldo = Convert.ToDecimal(cmd.ExecuteScalar() ?? 0);

        if (ownsConnection)
        {
            connection.Dispose();
        }

        return saldo;
    }

    public static void Fechar(string observacao)
    {
        using var conn = Database.GetConnection();
        conn.Open();
        int sessaoId = ObterSessaoAberta(conn)
            ?? throw new InvalidOperationException("Não existe caixa aberto para fechar.");
        decimal saldo = CalcularSaldoSessao(sessaoId, conn);

        const string sql = @"
            UPDATE CaixaSessoes
               SET DataFechamento = @data,
                   ValorFechamento = @valor,
                   Observacao = CASE WHEN @observacao = '' THEN Observacao ELSE @observacao END,
                   Status = 'Fechado'
             WHERE Id = @id";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        cmd.Parameters.AddWithValue("@valor", saldo);
        cmd.Parameters.AddWithValue("@observacao", observacao);
        cmd.Parameters.AddWithValue("@id", sessaoId);
        cmd.ExecuteNonQuery();
    }

    public static void RegistrarMovimento(
        SqliteConnection conn,
        SqliteTransaction transaction,
        string tipo,
        decimal valor,
        string descricao,
        string metodo,
        string cliente,
        string fornecedor,
        string origem,
        int? origemId)
    {
        int sessaoId = ObterSessaoAberta(conn, transaction)
            ?? throw new InvalidOperationException("Abra o caixa antes de registrar pagamentos ou recebimentos.");

        const string sql = @"
            INSERT INTO Caixa
                (Tipo, Valor, Descricao, DataMovimento, MetodoPagamento,
                 Cliente, Fornecedor, SessaoId, Origem, OrigemId)
            VALUES
                (@tipo, @valor, @descricao, @data, @metodo,
                 @cliente, @fornecedor, @sessao, @origem, @origemId)";
        using var cmd = new SqliteCommand(sql, conn, transaction);
        cmd.Parameters.AddWithValue("@tipo", tipo);
        cmd.Parameters.AddWithValue("@valor", valor);
        cmd.Parameters.AddWithValue("@descricao", descricao);
        cmd.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        cmd.Parameters.AddWithValue("@metodo", metodo);
        cmd.Parameters.AddWithValue("@cliente", cliente);
        cmd.Parameters.AddWithValue("@fornecedor", fornecedor);
        cmd.Parameters.AddWithValue("@sessao", sessaoId);
        cmd.Parameters.AddWithValue("@origem", origem);
        cmd.Parameters.AddWithValue("@origemId", origemId.HasValue ? origemId.Value : DBNull.Value);
        cmd.ExecuteNonQuery();
    }

    public static DataTable ListarMovimentosDoMes(out decimal entradas, out decimal saidas)
    {
        using var conn = Database.GetConnection();
        conn.Open();
        string inicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToString("yyyy-MM-dd");
        string fim = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).ToString("yyyy-MM-dd");

        const string sql = @"
            SELECT c.Id, c.DataMovimento AS Data, c.Tipo, c.Descricao,
                   c.Cliente, c.Fornecedor, c.MetodoPagamento AS Pagamento,
                   c.Valor, c.Origem
              FROM Caixa c
             WHERE c.DataMovimento >= @inicio AND c.DataMovimento < @fim
          ORDER BY c.DataMovimento DESC";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@inicio", inicio);
        cmd.Parameters.AddWithValue("@fim", fim);
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);

        entradas = Somar(conn, "Entrada", inicio, fim);
        saidas = Somar(conn, "Saida", inicio, fim);
        return table;
    }

    private static decimal Somar(SqliteConnection conn, string tipo, string inicio, string fim)
    {
        const string sql = @"
            SELECT IFNULL(SUM(Valor), 0) FROM Caixa
             WHERE Tipo = @tipo AND DataMovimento >= @inicio AND DataMovimento < @fim";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@tipo", tipo);
        cmd.Parameters.AddWithValue("@inicio", inicio);
        cmd.Parameters.AddWithValue("@fim", fim);
        return Convert.ToDecimal(cmd.ExecuteScalar());
    }
}
