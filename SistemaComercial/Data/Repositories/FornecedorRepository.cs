using Microsoft.Data.Sqlite;
using SistemaComercial.Models;

namespace SistemaComercial.Data.Repositories;

public static class FornecedorRepository
{
    public static List<Fornecedor> Listar(string termo = "")
    {
        var fornecedores = new List<Fornecedor>();
        using var conn = Database.GetConnection();
        conn.Open();

        const string sql = @"
            SELECT Id, Nome, Cnpj, Telefone, Email, Endereco
              FROM Fornecedor
             WHERE Nome LIKE @termo OR Cnpj LIKE @termo
          ORDER BY Nome COLLATE NOCASE, Id";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@termo", $"%{termo}%");
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            fornecedores.Add(new Fornecedor
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Cnpj = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Telefone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                Email = reader.IsDBNull(4) ? "" : reader.GetString(4),
                Endereco = reader.IsDBNull(5) ? "" : reader.GetString(5)
            });
        }

        return fornecedores;
    }

    public static void Inserir(Fornecedor fornecedor)
    {
        using var conn = Database.GetConnection();
        conn.Open();

        const string sql = @"
            INSERT INTO Fornecedor (Nome, Cnpj, Telefone, Email, Endereco)
            VALUES (@nome, @cnpj, @telefone, @email, @endereco)";

        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nome", fornecedor.Nome);
        cmd.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
        cmd.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
        cmd.Parameters.AddWithValue("@email", fornecedor.Email);
        cmd.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
        cmd.ExecuteNonQuery();
    }

    public static void Atualizar(Fornecedor fornecedor)
    {
        using var conn = Database.GetConnection();
        conn.Open();

        const string sql = @"
            UPDATE Fornecedor
               SET Nome = @nome,
                   Cnpj = @cnpj,
                   Telefone = @telefone,
                   Email = @email,
                   Endereco = @endereco
             WHERE Id = @id";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nome", fornecedor.Nome);
        cmd.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
        cmd.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
        cmd.Parameters.AddWithValue("@email", fornecedor.Email);
        cmd.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
        cmd.Parameters.AddWithValue("@id", fornecedor.Id);
        cmd.ExecuteNonQuery();
    }
}
