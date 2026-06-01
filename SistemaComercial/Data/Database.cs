using Microsoft.Data.Sqlite;

namespace SistemaComercial.Data;

public static class Database
{
    // O banco SQLite fica junto do executável do sistema.
    // Em uma apresentação, explique que isso simplifica o uso local sem servidor externo.
    private static readonly string connectionString =
        $"Data Source={AppDomain.CurrentDomain.BaseDirectory}estoque.db";

    // Cria uma conexão nova sempre que algum repositório precisa acessar o banco.
    public static SqliteConnection GetConnection()
    {
        return new SqliteConnection(connectionString);
    }

    public static void CriarTabela()
    {
        // Método chamado na inicialização do sistema.
        // Ele garante que as tabelas principais existam antes de qualquer tela consultar dados.
        using var conn = GetConnection();
        conn.Open();

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS Produtos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Preco REAL NOT NULL,
                Quantidade INTEGER NOT NULL,
                Validade TEXT
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS Vendas (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ProdutoId INTEGER NOT NULL,
                Quantidade INTEGER NOT NULL,
                DataVenda TEXT NOT NULL,
                MetodoPagamento TEXT NOT NULL,
                FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id)
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS Caixa (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Tipo TEXT NOT NULL,
                Valor REAL NOT NULL,
                Descricao TEXT,
                DataMovimento TEXT NOT NULL,
                MetodoPagamento TEXT NOT NULL
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS Clientes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                CpfCnpj TEXT,
                Telefone TEXT,
                Email TEXT,
                Endereco TEXT
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS Fornecedor (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Cnpj TEXT,
                Telefone TEXT,
                Email TEXT,
                Endereco TEXT
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS ContasReceber (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Cliente TEXT NOT NULL,
                Valor REAL NOT NULL,
                Data TEXT NOT NULL,
                Status TEXT NOT NULL
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS ContasPagar (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Fornecedor TEXT NOT NULL,
                Valor REAL NOT NULL,
                Data TEXT NOT NULL,
                Status TEXT NOT NULL
            );");

        AdicionarColunaSeNaoExistir(conn, "Caixa", "Cliente", "TEXT");
        AdicionarColunaSeNaoExistir(conn, "Vendas", "ClienteId", "INTEGER");
        AdicionarColunaSeNaoExistir(conn, "Caixa", "Fornecedor", "TEXT");
    }

    private static void Executar(SqliteConnection conn, string sql)
    {
        // Centraliza a execução de comandos SQL sem retorno.
        using var command = new SqliteCommand(sql, conn);
        command.ExecuteNonQuery();
    }

    private static void AdicionarColunaSeNaoExistir(SqliteConnection conn, string tabela, string coluna, string tipo)
    {
        // Pequena migração automática: adiciona uma coluna nova sem apagar dados antigos.
        if (ColunaExiste(conn, tabela, coluna))
        {
            return;
        }

        Executar(conn, $"ALTER TABLE {tabela} ADD COLUMN {coluna} {tipo};");
    }

    private static bool ColunaExiste(SqliteConnection conn, string tabela, string coluna)
    {
        // PRAGMA table_info lê a estrutura da tabela no SQLite.
        // Usamos isso para não tentar criar a mesma coluna duas vezes.
        using var command = new SqliteCommand($"PRAGMA table_info({tabela});", conn);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            if (string.Equals(reader["name"].ToString(), coluna, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
