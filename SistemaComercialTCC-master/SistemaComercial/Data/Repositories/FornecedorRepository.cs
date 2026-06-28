using Microsoft.Data.Sqlite;
using SistemaComercial.Models;

namespace SistemaComercial.Data.Repositories;

public static class FornecedorRepository
{
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
}
