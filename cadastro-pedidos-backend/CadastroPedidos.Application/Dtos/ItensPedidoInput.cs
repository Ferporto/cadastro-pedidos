using System.ComponentModel.DataAnnotations;

namespace CadastroPedidos.Application.Dtos;

public class ItensPedidoInput
{
    [Required]
    public int IdProduto { get; set; }
    [Required]
    public int Quantidade { get; set; }
}
