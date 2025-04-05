using SQLite;

namespace MinhasCompras.Models;

public class ProdutoDto
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    private string _descricao;

    public string Descricao
    {
        get { return _descricao; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Descrição não pode ser vazia ou nula.");
            _descricao = value;
        }
    }

    private double _qtd;

    public double Quantidade
    {
        get { return _qtd; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");
            _qtd = value;
        }
    }

    private double _preco;

    public double Preco
    {
        get { return _preco; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.");
            _preco = value;
        }
    }

    public double Total => Quantidade * Preco;
}
