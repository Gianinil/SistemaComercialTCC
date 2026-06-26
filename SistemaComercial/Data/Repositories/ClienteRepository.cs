using Microsoft.Data.Sqlite;
using SistemaComercial.Models;

namespace SistemaComercial.Data.Repositories;

public static class ClienteRepository
{
    public static List<Cliente> Listar(string termo = "")
    {
        var clientes = new List<Cliente>();
        using var conn = Database.GetConnection();
        conn.Open();

        const string sql = @"
            SELECT Id, Nome, CpfCnpj, Telefone, Email, Endereco
              FROM Clientes
             WHERE Nome LIKE @termo OR CpfCnpj LIKE @termo
          ORDER BY Nome COLLATE NOCASE, Id";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@termo", $"%{termo}%");
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            clientes.Add(new Cliente
            {
                Id = reader.GetInt32(0),
                Nome = reader.GetString(1),
                CpfCnpj = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Telefone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                Email = reader.IsDBNull(4) ? "" : reader.GetString(4),
                Endereco = reader.IsDBNull(5) ? "" : reader.GetString(5)
            });
        }

        return clientes;
    }

    public static void Inserir(Cliente cliente)
    {
        using var conn = Database.GetConnection();
        conn.Open();

        const string sql = @"
            INSERT INTO Clientes (Nome, CpfCnpj, Telefone, Email, Endereco)
            VALUES (@nome, @cpf, @telefone, @email, @endereco)";

        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nome", cliente.Nome);
        cmd.Parameters.AddWithValue("@cpf", cliente.CpfCnpj);
        cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
        cmd.Parameters.AddWithValue("@email", cliente.Email);
        cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
        cmd.ExecuteNonQuery();
    }

    public static void Atualizar(Cliente cliente)
    {
        using var conn = Database.GetConnection();
        conn.Open();

        const string sql = @"
            UPDATE Clientes
               SET Nome = @nome,
                   CpfCnpj = @cpf,
                   Telefone = @telefone,
                   Email = @email,
                   Endereco = @endereco
             WHERE Id = @id";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nome", cliente.Nome);
        cmd.Parameters.AddWithValue("@cpf", cliente.CpfCnpj);
        cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
        cmd.Parameters.AddWithValue("@email", cliente.Email);
        cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
        cmd.Parameters.AddWithValue("@id", cliente.Id);
        cmd.ExecuteNonQuery();
    }
}
