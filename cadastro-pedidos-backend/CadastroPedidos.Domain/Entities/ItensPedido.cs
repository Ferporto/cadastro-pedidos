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
}
