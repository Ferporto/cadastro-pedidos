using CadastroPedidos.Application.Dtos;

namespace CadastroPedidos.Application.Services
{
    public interface IProdutoService
    {
        Task<ProdutoOutput> Create(ProdutoInput input);
        Task<PagedResultDto<ProdutoOutput>> GetList(PagedFilteredAndSortedInput input);
        Task<ProdutoOutput?> Get(int id);
        Task Update(int id, ProdutoInput input);
        Task Delete(int id);
    }
}
