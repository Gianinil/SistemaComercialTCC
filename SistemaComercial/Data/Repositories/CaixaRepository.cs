using Microsoft.Data.Sqlite;
using SistemaComercial.Models;
using System.Data;

namespace SistemaComercial.Data.Repositories;

public static class CaixaRepository
{
    public static CaixaResumo ObterResumo()
    {
        using var conn = Database.GetConnection();
        conn.Open();

        var resumo = new CaixaResumo
        {
            Movimentos = CarregarMovimentos(conn),
            TotalEntradas = SomarPorTipo(conn, "Entrada"),
            TotalSaidas = SomarPorTipo(conn, "Saida")
        };

        return resumo;
    }

    private static DataTable CarregarMovimentos(SqliteConnection conn)
    {
        const string sql = @"
            SELECT Fornecedor, Cliente, Tipo, Valor, Descricao, DataMovimento, MetodoPagamento
              FROM Caixa
          ORDER BY DataMovimento DESC";

        using var cmd = new SqliteCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    private static decimal SomarPorTipo(SqliteConnection conn, string tipo)
    {
        using var cmd = new SqliteCommand("SELECT IFNULL(SUM(Valor), 0) FROM Caixa WHERE Tipo = @tipo", conn);
        cmd.Parameters.AddWithValue("@tipo", tipo);

        return Convert.ToDecimal(cmd.ExecuteScalar());
    }
}
