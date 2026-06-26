using Microsoft.Data.Sqlite;
using System.Data;

namespace SistemaComercial.Data;

public sealed class EstoqueResumo
{
    public int Produtos { get; init; }
    public int Unidades { get; init; }
    public int EstoqueBaixo { get; init; }
    public decimal ValorEstoque { get; init; }
    public DataTable Posicao { get; init; } = new();
    public DataTable Movimentacoes { get; init; } = new();
}

public static class EstoqueService
{
    public static EstoqueResumo ObterResumo(string termo = "")
    {
        using var conn = Database.GetConnection();
        conn.Open();
        var posicao = CarregarTabela(conn, @"
            SELECT p.Id, p.Nome AS Produto, p.Quantidade,
                   p.Preco AS PrecoVenda,
                   CASE
                       WHEN p.Quantidade = 0 THEN 'Sem estoque'
                       WHEN p.Quantidade <= 5 THEN 'Estoque baixo'
                       ELSE 'Normal'
                   END AS Situacao,
                   p.Validade
              FROM Produtos p
             WHERE p.Nome LIKE @termo
          ORDER BY CASE WHEN p.Quantidade <= 5 THEN 0 ELSE 1 END, p.Nome COLLATE NOCASE, p.Id", termo);

        var movimentos = CarregarTabela(conn, @"
            SELECT m.DataMovimento AS Data, p.Nome AS Produto, m.Tipo,
                   m.Quantidade, m.Origem, m.Observacao
              FROM EstoqueMovimentacoes m
              JOIN Produtos p ON p.Id = m.ProdutoId
          ORDER BY m.DataMovimento DESC
             LIMIT 100", null);

        using var cmd = new SqliteCommand(@"
            SELECT COUNT(*), IFNULL(SUM(Quantidade), 0),
                   IFNULL(SUM(CASE WHEN Quantidade <= 5 THEN 1 ELSE 0 END), 0),
                   IFNULL(SUM(Quantidade * Preco), 0)
              FROM Produtos", conn);
        using var reader = cmd.ExecuteReader();
        reader.Read();

        return new EstoqueResumo
        {
            Produtos = reader.GetInt32(0),
            Unidades = reader.GetInt32(1),
            EstoqueBaixo = reader.GetInt32(2),
            ValorEstoque = reader.GetDecimal(3),
            Posicao = posicao,
            Movimentacoes = movimentos
        };
    }

    private static DataTable CarregarTabela(SqliteConnection conn, string sql, string? termo)
    {
        using var cmd = new SqliteCommand(sql, conn);
        if (termo != null)
        {
            cmd.Parameters.AddWithValue("@termo", $"%{termo}%");
        }

        using var reader = cmd.ExecuteReader();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }
}
