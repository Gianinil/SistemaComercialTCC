using Microsoft.Data.Sqlite;
using SistemaComercial.Data;
using SistemaComercial.Data.Repositories;
using SistemaComercial.Models;

if (File.Exists(Database.DatabasePath)) File.Delete(Database.DatabasePath);
Database.CriarTabela();

using (var conn = Database.GetConnection())
{
    conn.Open();
    Execute(conn, "INSERT INTO Fornecedor (Nome, Cnpj, Telefone) VALUES ('Fornecedor Teste', '12345678000195', '11999999999')");
    Execute(conn, "INSERT INTO Fornecedor (Nome, Cnpj, Telefone) VALUES ('Alfa Fornecedor', '11222333000181', '11999999998')");
    Execute(conn, "INSERT INTO Clientes (Nome, CpfCnpj, Telefone) VALUES ('Cliente Teste', '52998224725', '11999999999')");
    Execute(conn, "INSERT INTO Clientes (Nome, CpfCnpj, Telefone) VALUES ('Ana Cliente', '11144477735', '11999999998')");
    Execute(conn, "INSERT INTO Produtos (Nome, Preco, Quantidade) VALUES ('Produto Teste', 25, 2)");
}

List<Cliente> clientes = ClienteRepository.Listar();
List<Fornecedor> fornecedores = FornecedorRepository.Listar();
Assert(clientes.Count == 2 && clientes[0].Nome == "Ana Cliente", "Clientes aparecem e ficam em ordem alfabética");
Assert(fornecedores.Count == 2 && fornecedores[0].Nome == "Alfa Fornecedor", "Fornecedores aparecem e ficam em ordem alfabética");

CaixaService.Abrir(100, "Teste automatizado");
int compraId = CompraService.Registrar(
    1, "Fornecedor Teste", DateTime.Today.AddDays(10), "Boleto", false, "Teste",
    new[] { new ItemCompra { ProdutoId = 1, ProdutoNome = "Produto Teste", Quantidade = 5, CustoUnitario = 10 } });

Assert(Scalar("SELECT Quantidade FROM Produtos WHERE Id = 1") == 7, "Compra atualiza estoque");
Assert(Scalar("SELECT COUNT(*) FROM ContasPagar WHERE Origem = 'Compra' AND OrigemId = 1") == 1, "Compra gera conta a pagar");

FinanceiroService.Liquidar(true, 1, "Pix");
Assert(Scalar("SELECT COUNT(*) FROM Caixa WHERE Tipo = 'Saida' AND Origem = 'ContaPagar'") == 1, "Pagamento movimenta caixa");

FinanceiroService.Salvar(false, null, "Cliente Teste", 80, DateTime.Today, "Pendente", "Venda teste", "Pix");
FinanceiroService.Liquidar(false, 1, "Pix");
Assert(Scalar("SELECT COUNT(*) FROM Caixa WHERE Tipo = 'Entrada' AND Origem = 'ContaReceber'") == 1, "Recebimento movimenta caixa");

string backupDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backups-teste");
string backup = BackupService.Criar(backupDir);
Assert(File.Exists(backup), "Backup criado");

CaixaService.Fechar("Teste concluído");
Assert(!CaixaService.ObterSessaoAberta().HasValue, "Caixa fecha corretamente");
Console.WriteLine($"OK compra={compraId}; estoque=7; pagar=Pago; receber=Pago; backup={Path.GetFileName(backup)}");

static void Execute(SqliteConnection conn, string sql)
{
    using var cmd = new SqliteCommand(sql, conn);
    cmd.ExecuteNonQuery();
}

static long Scalar(string sql)
{
    using var conn = Database.GetConnection();
    conn.Open();
    using var cmd = new SqliteCommand(sql, conn);
    return Convert.ToInt64(cmd.ExecuteScalar());
}

static void Assert(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException("Falha: " + message);
    Console.WriteLine("OK: " + message);
}
