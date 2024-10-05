using CadastroPedidos.Application.Dtos;

namespace CadastroPedidos.Application.Services
{
    public interface IPedidoService
    {
        Task<PedidoOutput> Create(PedidoInput input);
        Task<PagedResultDto<PedidoOutput>> GetList(PagedInput input);
        Task<PedidoOutput?> Get(int id);
        Task Update(int id, PedidoInput input);
        Task Delete(int id);
    }
}
