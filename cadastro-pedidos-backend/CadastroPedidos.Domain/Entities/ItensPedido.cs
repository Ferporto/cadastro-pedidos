using Azure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroPedidos.Domain.Entities;

public class ItensPedido
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Pedido))]
    public int IdPedido { get; set; }

    [ForeignKey(nameof(Produto))]
    public int IdProduto { get; set; }

    [Required]
    public int Quantidade { get; set; }

    // Relacionamento com Pedido
    public virtual Pedido Pedido { get; set; }

    // Relacionamento com Produto
    public virtual Produto Produto { get; set; }

    public ItensPedido()
    {
    }

    public ItensPedido(int idPedido, int idProduto, int quantidade)
    {
        IdPedido = idPedido;
        IdProduto = idProduto;
        Quantidade = quantidade;
    }

    public void Atualizar(int idPedido, int idProduto, int quantidade)
    {
        IdPedido = idPedido;
        IdProduto = idProduto;
        Quantidade = quantidade;
    }
}
