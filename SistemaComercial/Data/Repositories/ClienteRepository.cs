using Microsoft.Data.Sqlite;
using SistemaComercial.Models;

namespace SistemaComercial.Data.Repositories;

public static class ClienteRepository
{
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
}
