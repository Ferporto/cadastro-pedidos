using System.ComponentModel.DataAnnotations;

namespace CadastroPedidos.Domain.Entities;

public class Pedido
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(60)]
    public string NomeCliente { get; set; }

    [Required]
    [StringLength(60)]
    [EmailAddress]
    public string EmailCliente { get; set; }

    public DateTime DataCricao { get; set; } = DateTime.UtcNow;
    
    public bool Pago { get; set; }

    // Relacionamento com ItensPedido
    public virtual ICollection<ItensPedido> ItensPedido { get; set; }

    public Pedido()
    {
    }

    public Pedido(string nomeCliente, string emailCliente, bool pago)
    {
        NomeCliente = nomeCliente;
        EmailCliente = emailCliente;
        Pago = pago;
    }

    public void Atualizar(string nomeCliente, string emailCliente, bool pago)
    {
        NomeCliente = nomeCliente;
        EmailCliente = emailCliente;
        Pago = pago;
    }
}
