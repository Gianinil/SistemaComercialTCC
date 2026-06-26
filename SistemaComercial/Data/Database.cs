using Microsoft.Data.Sqlite;

namespace SistemaComercial.Data;

public static class Database
{
    private static readonly string databasePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "estoque.db");

    private static readonly string connectionString = $"Data Source={databasePath}";

    public static string DatabasePath => databasePath;

    public static SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(connectionString);
        return connection;
    }

    public static void CriarTabela()
    {
        using var conn = GetConnection();
        conn.Open();

        Executar(conn, "PRAGMA foreign_keys = ON;");
        Executar(conn, "PRAGMA journal_mode = WAL;");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS Produtos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Preco REAL NOT NULL,
                Quantidade INTEGER NOT NULL DEFAULT 0,
                Validade TEXT
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
            CREATE TABLE IF NOT EXISTS Vendas (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ProdutoId INTEGER NOT NULL,
                Quantidade INTEGER NOT NULL,
                DataVenda TEXT NOT NULL,
                MetodoPagamento TEXT NOT NULL,
                ClienteId INTEGER,
                FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id),
                FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS CaixaSessoes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                DataAbertura TEXT NOT NULL,
                ValorInicial REAL NOT NULL DEFAULT 0,
                DataFechamento TEXT,
                ValorFechamento REAL,
                Observacao TEXT,
                Status TEXT NOT NULL DEFAULT 'Aberto'
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS Caixa (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Tipo TEXT NOT NULL,
                Valor REAL NOT NULL,
                Descricao TEXT,
                DataMovimento TEXT NOT NULL,
                MetodoPagamento TEXT NOT NULL,
                Cliente TEXT,
                Fornecedor TEXT,
                SessaoId INTEGER,
                Origem TEXT,
                OrigemId INTEGER,
                FOREIGN KEY (SessaoId) REFERENCES CaixaSessoes(Id)
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS ContasReceber (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Cliente TEXT NOT NULL,
                Valor REAL NOT NULL,
                Data TEXT NOT NULL,
                Status TEXT NOT NULL,
                Descricao TEXT,
                DataPagamento TEXT,
                MetodoPagamento TEXT,
                Origem TEXT,
                OrigemId INTEGER
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS ContasPagar (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Fornecedor TEXT NOT NULL,
                Valor REAL NOT NULL,
                Data TEXT NOT NULL,
                Status TEXT NOT NULL,
                Descricao TEXT,
                DataPagamento TEXT,
                MetodoPagamento TEXT,
                Origem TEXT,
                OrigemId INTEGER
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS Compras (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FornecedorId INTEGER NOT NULL,
                DataCompra TEXT NOT NULL,
                DataVencimento TEXT NOT NULL,
                MetodoPagamento TEXT NOT NULL,
                Status TEXT NOT NULL,
                ValorTotal REAL NOT NULL,
                Observacao TEXT,
                FOREIGN KEY (FornecedorId) REFERENCES Fornecedor(Id)
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS ItensCompra (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                CompraId INTEGER NOT NULL,
                ProdutoId INTEGER NOT NULL,
                Quantidade INTEGER NOT NULL,
                CustoUnitario REAL NOT NULL,
                FOREIGN KEY (CompraId) REFERENCES Compras(Id),
                FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id)
            );");

        Executar(conn, @"
            CREATE TABLE IF NOT EXISTS EstoqueMovimentacoes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ProdutoId INTEGER NOT NULL,
                Tipo TEXT NOT NULL,
                Quantidade INTEGER NOT NULL,
                DataMovimento TEXT NOT NULL,
                Origem TEXT NOT NULL,
                OrigemId INTEGER,
                Observacao TEXT,
                FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id)
            );");

        // Migra bancos criados por versões anteriores sem apagar dados.
        AdicionarColunaSeNaoExistir(conn, "Vendas", "ClienteId", "INTEGER");
        AdicionarColunaSeNaoExistir(conn, "Caixa", "Cliente", "TEXT");
        AdicionarColunaSeNaoExistir(conn, "Caixa", "Fornecedor", "TEXT");
        AdicionarColunaSeNaoExistir(conn, "Caixa", "SessaoId", "INTEGER");
        AdicionarColunaSeNaoExistir(conn, "Caixa", "Origem", "TEXT");
        AdicionarColunaSeNaoExistir(conn, "Caixa", "OrigemId", "INTEGER");

        foreach (string tabela in new[] { "ContasPagar", "ContasReceber" })
        {
            AdicionarColunaSeNaoExistir(conn, tabela, "Descricao", "TEXT");
            AdicionarColunaSeNaoExistir(conn, tabela, "DataPagamento", "TEXT");
            AdicionarColunaSeNaoExistir(conn, tabela, "MetodoPagamento", "TEXT");
            AdicionarColunaSeNaoExistir(conn, tabela, "Origem", "TEXT");
            AdicionarColunaSeNaoExistir(conn, tabela, "OrigemId", "INTEGER");
        }

        Executar(conn, "CREATE INDEX IF NOT EXISTS IX_Caixa_Data ON Caixa(DataMovimento);");
        Executar(conn, "CREATE INDEX IF NOT EXISTS IX_Estoque_Produto_Data ON EstoqueMovimentacoes(ProdutoId, DataMovimento);");
        Executar(conn, "CREATE INDEX IF NOT EXISTS IX_ContasPagar_Status_Data ON ContasPagar(Status, Data);");
        Executar(conn, "CREATE INDEX IF NOT EXISTS IX_ContasReceber_Status_Data ON ContasReceber(Status, Data);");
    }

    private static void Executar(SqliteConnection conn, string sql)
    {
        using var command = new SqliteCommand(sql, conn);
        command.ExecuteNonQuery();
    }

    private static void AdicionarColunaSeNaoExistir(
        SqliteConnection conn,
        string tabela,
        string coluna,
        string tipo)
    {
        if (!ColunaExiste(conn, tabela, coluna))
        {
            Executar(conn, $"ALTER TABLE {tabela} ADD COLUMN {coluna} {tipo};");
        }
    }

    private static bool ColunaExiste(SqliteConnection conn, string tabela, string coluna)
    {
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
