namespace CadastroPedidos.Application.Dtos;

public class PagedInput
{
    public int SkipCount { get; set; }
    public int MaxResultCount { get; set; }

    public PagedInput()
    {
        SkipCount = 0;
        MaxResultCount = 25;
    }
}
