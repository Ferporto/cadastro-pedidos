using CadastroPedidos.Application.Dtos;

namespace CadastroPedidos.Application.Services
{
    public interface IPedidoService
    {
        Task Create(PedidoInput input);
        Task<PagedResultDto<PedidoOutput>> GetList(PagedFilteredAndSortedInput input);
        Task<PedidoOutput?> Get(int id);
        Task Update(int id, PedidoInput input);
        Task Delete(int id);
    }
}
