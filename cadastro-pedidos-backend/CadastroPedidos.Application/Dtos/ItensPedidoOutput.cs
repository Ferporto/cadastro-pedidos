using CadastroPedidos.Domain.Entities;

namespace CadastroPedidos.Application.Dtos;

public class ItensPedidoOutput
{
    public int Id { get; set; }
    public int IdPedido { get; set; }
    public int IdProduto { get; set; }
    public string NomeProduto { get; set; }
    public decimal ValorUnitario { get; set; }
    public int Quantidade { get; set; }

    public ItensPedidoOutput()
    {
    }

    public ItensPedidoOutput(ItensPedido itensPedido)
    {
        Id = itensPedido.Id;
        IdPedido = itensPedido.IdPedido;
        IdProduto = itensPedido.IdProduto;
        NomeProduto = itensPedido.Produto.NomeProduto;
        ValorUnitario = itensPedido.Produto.Valor;
        Quantidade = itensPedido.Quantidade;
    }
}
