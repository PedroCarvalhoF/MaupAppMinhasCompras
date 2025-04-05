using SQLite;

namespace MinhasCompras.Models;

public class ProdutoDto
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public double Quantidade { get; set; }
    public double Preco { get; set; }
}
