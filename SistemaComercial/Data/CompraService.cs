using Microsoft.Data.Sqlite;
using SistemaComercial.Models;

namespace SistemaComercial.Data;

public static class CompraService
{
    public static int Registrar(
        int fornecedorId,
        string fornecedorNome,
        DateTime vencimento,
        string metodoPagamento,
        bool pago,
        string observacao,
        IReadOnlyCollection<ItemCompra> itens)
    {
        if (itens.Count == 0)
        {
            throw new InvalidOperationException("Adicione pelo menos um produto à compra.");
        }

        decimal total = itens.Sum(item => item.Total);
        using var conn = Database.GetConnection();
        conn.Open();
        using var transaction = conn.BeginTransaction();

        try
        {
            const string sqlCompra = @"
                INSERT INTO Compras
                    (FornecedorId, DataCompra, DataVencimento, MetodoPagamento,
                     Status, ValorTotal, Observacao)
                VALUES (@fornecedor, @data, @vencimento, @metodo, @status, @total, @observacao);
                SELECT last_insert_rowid();";
            using var cmdCompra = new SqliteCommand(sqlCompra, conn, transaction);
            cmdCompra.Parameters.AddWithValue("@fornecedor", fornecedorId);
            cmdCompra.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmdCompra.Parameters.AddWithValue("@vencimento", vencimento.ToString("yyyy-MM-dd"));
            cmdCompra.Parameters.AddWithValue("@metodo", metodoPagamento);
            cmdCompra.Parameters.AddWithValue("@status", pago ? "Pago" : "Pendente");
            cmdCompra.Parameters.AddWithValue("@total", total);
            cmdCompra.Parameters.AddWithValue("@observacao", observacao);
            int compraId = Convert.ToInt32((long)(cmdCompra.ExecuteScalar() ?? 0L));

            foreach (ItemCompra item in itens)
            {
                const string sqlItem = @"
                    INSERT INTO ItensCompra (CompraId, ProdutoId, Quantidade, CustoUnitario)
                    VALUES (@compra, @produto, @quantidade, @custo)";
                using var cmdItem = new SqliteCommand(sqlItem, conn, transaction);
                cmdItem.Parameters.AddWithValue("@compra", compraId);
                cmdItem.Parameters.AddWithValue("@produto", item.ProdutoId);
                cmdItem.Parameters.AddWithValue("@quantidade", item.Quantidade);
                cmdItem.Parameters.AddWithValue("@custo", item.CustoUnitario);
                cmdItem.ExecuteNonQuery();

                using var cmdEstoque = new SqliteCommand(
                    "UPDATE Produtos SET Quantidade = Quantidade + @quantidade WHERE Id = @produto", conn, transaction);
                cmdEstoque.Parameters.AddWithValue("@quantidade", item.Quantidade);
                cmdEstoque.Parameters.AddWithValue("@produto", item.ProdutoId);
                cmdEstoque.ExecuteNonQuery();

                const string sqlMovimento = @"
                    INSERT INTO EstoqueMovimentacoes
                        (ProdutoId, Tipo, Quantidade, DataMovimento, Origem, OrigemId, Observacao)
                    VALUES (@produto, 'Entrada', @quantidade, @data, 'Compra', @compra, @observacao)";
                using var cmdMovimento = new SqliteCommand(sqlMovimento, conn, transaction);
                cmdMovimento.Parameters.AddWithValue("@produto", item.ProdutoId);
                cmdMovimento.Parameters.AddWithValue("@quantidade", item.Quantidade);
                cmdMovimento.Parameters.AddWithValue("@data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmdMovimento.Parameters.AddWithValue("@compra", compraId);
                cmdMovimento.Parameters.AddWithValue("@observacao", $"Compra de {fornecedorNome}");
                cmdMovimento.ExecuteNonQuery();
            }

            const string sqlConta = @"
                INSERT INTO ContasPagar
                    (Fornecedor, Valor, Data, Status, Descricao, DataPagamento,
                     MetodoPagamento, Origem, OrigemId)
                VALUES (@fornecedor, @valor, @data, @status, @descricao, @pagamento,
                        @metodo, 'Compra', @compra);
                SELECT last_insert_rowid();";
            using var cmdConta = new SqliteCommand(sqlConta, conn, transaction);
            cmdConta.Parameters.AddWithValue("@fornecedor", fornecedorNome);
            cmdConta.Parameters.AddWithValue("@valor", total);
            cmdConta.Parameters.AddWithValue("@data", vencimento.ToString("yyyy-MM-dd"));
            cmdConta.Parameters.AddWithValue("@status", pago ? "Pago" : "Pendente");
            cmdConta.Parameters.AddWithValue("@descricao", $"Compra nº {compraId}");
            cmdConta.Parameters.AddWithValue("@pagamento", pago ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : DBNull.Value);
            cmdConta.Parameters.AddWithValue("@metodo", metodoPagamento);
            cmdConta.Parameters.AddWithValue("@compra", compraId);
            int contaId = Convert.ToInt32((long)(cmdConta.ExecuteScalar() ?? 0L));

            if (pago)
            {
                CaixaService.RegistrarMovimento(
                    conn, transaction, "Saida", total, $"Compra nº {compraId}",
                    metodoPagamento, "", fornecedorNome, "ContaPagar", contaId);
            }

            transaction.Commit();
            return compraId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
