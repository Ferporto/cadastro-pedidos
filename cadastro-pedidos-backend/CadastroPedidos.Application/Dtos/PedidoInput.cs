using System.ComponentModel.DataAnnotations;

namespace CadastroPedidos.Application.Dtos;

public class PedidoInput
{
    [Required]
    public string NomeCliente { get; set; }
    [Required]
    public string EmailCliente { get; set; }
    [Required]
    public bool Pago { get; set; }
}
