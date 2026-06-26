using System.Data;

namespace SistemaComercial.Models;

public class CaixaResumo
{
    public DataTable Movimentos { get; set; } = new();
    public decimal TotalEntradas { get; set; }
    public decimal TotalSaidas { get; set; }
    public decimal Saldo => TotalEntradas - TotalSaidas;
}
