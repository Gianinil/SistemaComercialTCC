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

        using var cmd = new SqliteCommand("SELECT * FROM Produtos ORDER BY Nome COLLATE NOCASE, Id", conn);
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

        using var transaction = conn.BeginTransaction();
        const string sql = @"
            INSERT INTO Produtos (Nome, Preco, Quantidade, Validade)
            VALUES (@nome, @preco, @quantidade, @validade);
            SELECT last_insert_rowid();";

        using var cmd = new SqliteCommand(sql, conn, transaction);
        PreencherParametrosProduto(cmd, produto);
        int produtoId = Convert.ToInt32((long)(cmd.ExecuteScalar() ?? 0L));

        if (produto.Quantidade > 0)
        {
            RegistrarMovimento(conn, transaction, produtoId, "Entrada", produto.Quantidade, "Cadastro", null, "Estoque inicial");
        }

        transaction.Commit();
    }

    public static void Atualizar(Produto produto)
    {
        // Atualiza o registro pelo Id, mantendo o mesmo produto na base.
        using var conn = Database.GetConnection();
        conn.Open();

        using var transaction = conn.BeginTransaction();
        int quantidadeAnterior;
        using (var cmdAnterior = new SqliteCommand("SELECT Quantidade FROM Produtos WHERE Id = @id", conn, transaction))
        {
            cmdAnterior.Parameters.AddWithValue("@id", produto.Id);
            quantidadeAnterior = Convert.ToInt32(cmdAnterior.ExecuteScalar() ?? 0);
        }

        const string sql = @"
            UPDATE Produtos
               SET Nome = @nome,
                   Preco = @preco,
                   Quantidade = @quantidade,
                   Validade = @validade
             WHERE Id = @id";

        using var cmd = new SqliteCommand(sql, conn, transaction);
        PreencherParametrosProduto(cmd, produto);
        cmd.Parameters.AddWithValue("@id", produto.Id);
        cmd.ExecuteNonQuery();

        int diferenca = produto.Quantidade - quantidadeAnterior;
        if (diferenca != 0)
        {
            RegistrarMovimento(
                conn, transaction, produto.Id,
                diferenca > 0 ? "Entrada" : "Saida",
                Math.Abs(diferenca), "Ajuste manual", null,
                "Alteração realizada no cadastro do produto");
        }

        transaction.Commit();
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

        const string sql = "SELECT * FROM Produtos WHERE Nome LIKE @termo ORDER BY Nome COLLATE NOCASE, Id";
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

    private static void RegistrarMovimento(
        SqliteConnection conn,
        SqliteTransaction transaction,
        int produtoId,
        string tipo,
        int quantidade,
        string origem,
        int? origemId,
        string observacao)
    {
        const string sql = @"
            INSERT INTO EstoqueMovimentacoes
                (ProdutoId, Tipo, Quantidade, DataMovimento, Origem, OrigemId, Observacao)
            VALUES (@produto, @tipo, @quantidade, @data, @origem, @origemId, @observacao)";
        using var cmd = new SqliteCommand(sql, conn, transaction);
        cmd.Parameters.AddWithValue("@produto", produtoId);
        cmd.Parameters.AddWithValue("@tipo", tipo);
        cmd.Parameters.AddWithValue("@quantidade", quantidade);
        cmd.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        cmd.Parameters.AddWithValue("@origem", origem);
        cmd.Parameters.AddWithValue("@origemId", origemId.HasValue ? origemId.Value : DBNull.Value);
        cmd.Parameters.AddWithValue("@observacao", observacao);
        cmd.ExecuteNonQuery();
    }
}
