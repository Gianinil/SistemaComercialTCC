using Microsoft.Data.Sqlite;
using System.Data;

namespace SistemaComercial.Data;

public static class FinanceiroService
{
    public static DataTable Listar(bool pagar)
    {
        string tabela = pagar ? "ContasPagar" : "ContasReceber";
        string pessoa = pagar ? "Fornecedor" : "Cliente";
        using var conn = Database.GetConnection();
        conn.Open();

        string sql = $@"
            SELECT Id, {pessoa}, Descricao, Valor,
                   Data AS Vencimento, Status, MetodoPagamento AS Pagamento,
                   DataPagamento, Origem
              FROM {tabela}
          ORDER BY CASE WHEN Status = 'Pendente' THEN 0 ELSE 1 END,
                   Data, {pessoa} COLLATE NOCASE, Id";
        using var cmd = new SqliteCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    public static void Salvar(
        bool pagar,
        int? id,
        string pessoa,
        decimal valor,
        DateTime vencimento,
        string status,
        string descricao,
        string metodo)
    {
        string tabela = pagar ? "ContasPagar" : "ContasReceber";
        string colunaPessoa = pagar ? "Fornecedor" : "Cliente";
        using var conn = Database.GetConnection();
        conn.Open();
        using var transaction = conn.BeginTransaction();

        try
        {
            if (id.HasValue)
            {
                string statusAnterior;
                using (var cmdAnterior = new SqliteCommand($"SELECT Status FROM {tabela} WHERE Id = @id", conn, transaction))
                {
                    cmdAnterior.Parameters.AddWithValue("@id", id.Value);
                    statusAnterior = cmdAnterior.ExecuteScalar()?.ToString() ?? "Pendente";
                }

                if (statusAnterior == "Pago")
                {
                    throw new InvalidOperationException("Contas já pagas permanecem bloqueadas para preservar o histórico do caixa.");
                }

                string sql = $@"
                    UPDATE {tabela}
                       SET {colunaPessoa} = @pessoa,
                           Valor = @valor,
                           Data = @data,
                           Status = @status,
                           Descricao = @descricao,
                           MetodoPagamento = @metodo
                     WHERE Id = @id";
                using var cmd = new SqliteCommand(sql, conn, transaction);
                Preencher(cmd, pessoa, valor, vencimento, status, descricao, metodo);
                cmd.Parameters.AddWithValue("@id", id.Value);
                cmd.ExecuteNonQuery();

                if (statusAnterior != "Pago" && status == "Pago")
                {
                    RegistrarLiquidacao(conn, transaction, pagar, id.Value, pessoa, valor, descricao, metodo);
                }
            }
            else
            {
                string sql = $@"
                    INSERT INTO {tabela}
                        ({colunaPessoa}, Valor, Data, Status, Descricao,
                         DataPagamento, MetodoPagamento, Origem)
                    VALUES (@pessoa, @valor, @data, @status, @descricao,
                            @pagamento, @metodo, 'Manual');
                    SELECT last_insert_rowid();";
                using var cmd = new SqliteCommand(sql, conn, transaction);
                Preencher(cmd, pessoa, valor, vencimento, status, descricao, metodo);
                cmd.Parameters.AddWithValue("@pagamento", status == "Pago"
                    ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    : DBNull.Value);
                int novoId = Convert.ToInt32((long)(cmd.ExecuteScalar() ?? 0L));

                if (status == "Pago")
                {
                    RegistrarLiquidacao(conn, transaction, pagar, novoId, pessoa, valor, descricao, metodo);
                }
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public static void Liquidar(bool pagar, int id, string metodo)
    {
        string tabela = pagar ? "ContasPagar" : "ContasReceber";
        string pessoaColuna = pagar ? "Fornecedor" : "Cliente";
        using var conn = Database.GetConnection();
        conn.Open();
        using var transaction = conn.BeginTransaction();

        try
        {
            string sql = $"SELECT {pessoaColuna}, Valor, Status, Descricao FROM {tabela} WHERE Id = @id";
            using var cmd = new SqliteCommand(sql, conn, transaction);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                throw new InvalidOperationException("Conta não encontrada.");
            }

            string pessoa = reader.GetString(0);
            decimal valor = reader.GetDecimal(1);
            string status = reader.GetString(2);
            string descricao = reader.IsDBNull(3) ? "" : reader.GetString(3);
            reader.Close();

            if (status == "Pago")
            {
                throw new InvalidOperationException("Essa conta já foi liquidada.");
            }

            using var cmdUpdate = new SqliteCommand($@"
                UPDATE {tabela}
                   SET Status = 'Pago', DataPagamento = @data, MetodoPagamento = @metodo
                 WHERE Id = @id", conn, transaction);
            cmdUpdate.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmdUpdate.Parameters.AddWithValue("@metodo", metodo);
            cmdUpdate.Parameters.AddWithValue("@id", id);
            cmdUpdate.ExecuteNonQuery();

            RegistrarLiquidacao(conn, transaction, pagar, id, pessoa, valor, descricao, metodo);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static void RegistrarLiquidacao(
        SqliteConnection conn,
        SqliteTransaction transaction,
        bool pagar,
        int id,
        string pessoa,
        decimal valor,
        string descricao,
        string metodo)
    {
        CaixaService.RegistrarMovimento(
            conn,
            transaction,
            pagar ? "Saida" : "Entrada",
            valor,
            string.IsNullOrWhiteSpace(descricao)
                ? (pagar ? $"Pagamento a {pessoa}" : $"Recebimento de {pessoa}")
                : descricao,
            metodo,
            pagar ? "" : pessoa,
            pagar ? pessoa : "",
            pagar ? "ContaPagar" : "ContaReceber",
            id);
    }

    private static void Preencher(
        SqliteCommand cmd,
        string pessoa,
        decimal valor,
        DateTime vencimento,
        string status,
        string descricao,
        string metodo)
    {
        cmd.Parameters.AddWithValue("@pessoa", pessoa);
        cmd.Parameters.AddWithValue("@valor", valor);
        cmd.Parameters.AddWithValue("@data", vencimento.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@status", status);
        cmd.Parameters.AddWithValue("@descricao", descricao);
        cmd.Parameters.AddWithValue("@metodo", metodo);
    }
}
