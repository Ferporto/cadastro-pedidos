using CadastroPedidos.Domain.Entities;

namespace CadastroPedidos.Application.Dtos;

public class ProdutoOutput
{
    public int Id { get; set; }
    public string NomeProduto { get; set; }
    public decimal Valor { get; set; }

    public ProdutoOutput()
    {
    }

    public ProdutoOutput(Produto produto)
    {
        Id = produto.Id;
        NomeProduto = produto.NomeProduto;
        Valor = produto.Valor;
    }
}
