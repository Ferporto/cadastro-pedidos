using CadastroPedidos.Domain.Entities;

namespace CadastroPedidos.Application.Dtos;

public class PedidoOutput
{
    public int Id { get; set; }
    public string NomeCliente { get; set; }
    public string EmailCliente { get; set; }
    public bool Pago { get; set; }
    public List<ItensPedidoOutput> ItensPedido { get; set; }

    public PedidoOutput()
    {
        ItensPedido = new List<ItensPedidoOutput>();
    }

    public PedidoOutput(Pedido pedido)
    {
        Id = pedido.Id;
        NomeCliente = pedido.NomeCliente;
        EmailCliente = pedido.EmailCliente;
        Pago = pedido.Pago;
        ItensPedido = pedido.ItensPedido == null
            ? new List<ItensPedidoOutput>()
            : pedido.ItensPedido.Select(item => new ItensPedidoOutput(item)).ToList();
    }
}
