namespace CadastroPedidos.Application.Dtos;

public class PagedResultDto<T> where T : class
{
    public List<T> Itens { get; set; }
    public int TotalCount { get; set; }

    public PagedResultDto(List<T> itens, int totalCount)
    {
        Itens = itens;
        TotalCount = totalCount;
    }
}
