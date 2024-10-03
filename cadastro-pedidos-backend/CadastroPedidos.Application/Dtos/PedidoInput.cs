namespace CadastroPedidos.Application.Dtos;

public class PedidoInput
{
    public string NomeCliente { get; set; }
    public string EmailCliente { get; set; }
    public bool Pago { get; set; }
}
