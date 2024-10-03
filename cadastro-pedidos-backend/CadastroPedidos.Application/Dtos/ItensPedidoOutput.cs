using CadastroPedidos.Domain.Entities;

namespace CadastroPedidos.Application.Dtos;

public class ItensPedidoOutput
{
    public int Id { get; set; }
    public string NomeProduto { get; set; }
    public decimal ValorUnitario { get; set; }
    public int Quantidade { get; set; }

    public ItensPedidoOutput()
    {
    }

    public ItensPedidoOutput(ItensPedido itensPedido)
    {
        Id = itensPedido.Id;
        NomeProduto = itensPedido.Produto.NomeProduto;
        ValorUnitario = itensPedido.Produto.Valor;
        Quantidade = itensPedido.Quantidade;
    }
}
