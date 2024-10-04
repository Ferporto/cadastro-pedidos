using System.ComponentModel.DataAnnotations;

namespace CadastroPedidos.Application.Dtos;

public class ProdutoInput
{
    [Required]
    public string NomeProduto { get; set; }
    [Required]
    public decimal Valor { get; set; }
}
