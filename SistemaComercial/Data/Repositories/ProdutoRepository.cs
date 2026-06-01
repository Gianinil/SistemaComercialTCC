using Microsoft.Data.Sqlite;
using SistemaComercial.Models;

namespace SistemaComercial.Data.Repositories;

public static class ProdutoRepository
{
    // Repository concentra o acesso ao banco da entidade Produto.
    // Assim as telas n?o precisam saber SQL e ficam respons?veis s? pela interface.
    public static List<Produto> Listar()
    {
        var produtos = new List<Produto>();

        using var conn = Database.GetConnection();
        conn.Open();

        using var cmd = new SqliteCommand("SELECT * FROM Produtos ORDER BY Nome", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            produtos.Add(MapearProduto(reader));
        }

        return produtos;
    }

    public static void Inserir(Produto produto)
    {
        // Insere usando par?metros para evitar problemas com aspas e reduzir risco de SQL injection.
        using var conn = Database.GetConnection();
        conn.Open();

        const string sql = @"
            INSERT INTO Produtos (Nome, Preco, Quantidade, Validade)
            VALUES (@nome, @preco, @quantidade, @validade)";

        using var cmd = new SqliteCommand(sql, conn);
        PreencherParametrosProduto(cmd, produto);
        cmd.ExecuteNonQuery();
    }

    public static void Atualizar(Produto produto)
    {
        // Atualiza o registro pelo Id, mantendo o mesmo produto na base.
        using var conn = Database.GetConnection();
        conn.Open();

        const string sql = @"
            UPDATE Produtos
               SET Nome = @nome,
                   Preco = @preco,
                   Quantidade = @quantidade,
                   Validade = @validade
             WHERE Id = @id";

        using var cmd = new SqliteCommand(sql, conn);
        PreencherParametrosProduto(cmd, produto);
        cmd.Parameters.AddWithValue("@id", produto.Id);
        cmd.ExecuteNonQuery();
    }

    public static void Excluir(int id)
    {
        // Exclui o produto. Se ele estiver vinculado a vendas, o banco pode bloquear a exclus?o.
        using var conn = Database.GetConnection();
        conn.Open();

        using var cmd = new SqliteCommand("DELETE FROM Produtos WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (SqliteException ex)
        {
            throw new InvalidOperationException("Não é possível excluir. Produto possui registros vinculados.", ex);
        }
    }

    public static List<Produto> Buscar(string termo)
    {
        // Busca por nome usando LIKE, permitindo pesquisar apenas parte do texto.
        var produtos = new List<Produto>();

        using var conn = Database.GetConnection();
        conn.Open();

        const string sql = "SELECT * FROM Produtos WHERE Nome LIKE @termo ORDER BY Nome";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@termo", $"%{termo}%");

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            produtos.Add(MapearProduto(reader));
        }

        return produtos;
    }

    private static Produto MapearProduto(SqliteDataReader reader)
    {
        // Transforma uma linha do banco em objeto C#.
        // Esse mapeamento deixa o restante do sistema trabalhar com Produto, n?o com colunas soltas.
        return new Produto
        {
            Id = Convert.ToInt32(reader["Id"]),
            Nome = reader["Nome"].ToString() ?? string.Empty,
            Preco = Convert.ToDecimal(reader["Preco"]),
            Quantidade = Convert.ToInt32(reader["Quantidade"]),
            Validade = reader["Validade"] != DBNull.Value
                ? DateTime.Parse(reader["Validade"].ToString() ?? string.Empty)
                : DateTime.MinValue
        };
    }

    private static void PreencherParametrosProduto(SqliteCommand cmd, Produto produto)
    {
        // Evita repeti??o: inserir e atualizar usam os mesmos campos.
        cmd.Parameters.AddWithValue("@nome", produto.Nome);
        cmd.Parameters.AddWithValue("@preco", produto.Preco);
        cmd.Parameters.AddWithValue("@quantidade", produto.Quantidade);
        cmd.Parameters.AddWithValue("@validade", produto.Validade);
    }
}
