namespace SistemaComercial.Models;

public class ItemCompra
{
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal CustoUnitario { get; set; }
    public decimal Total => Quantidade * CustoUnitario;
}
