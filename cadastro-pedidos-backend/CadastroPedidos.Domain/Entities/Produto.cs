using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroPedidos.Domain.Entities;

public class Produto
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string NomeProduto { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Valor { get; set; }

    // Relacionamento com ItensPedido
    public virtual ICollection<ItensPedido> ItensPedido { get; set; }

    public Produto()
    {
    }

    public Produto(string nomeProduto, decimal valor)
    {
        NomeProduto = nomeProduto;
        Valor = valor;
    }

    public void Atualizar(string nomeProduto, decimal valor)
    {
        NomeProduto = nomeProduto;
        Valor = valor;
    }
}

