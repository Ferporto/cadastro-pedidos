using CadastroPedidos.Application.Dtos;

namespace CadastroPedidos.Application.Services
{
    public interface IItensPedidoService
    {
        Task<ItensPedidoOutput> Create(int idPedido, ItensPedidoInput input);
        Task<PagedResultDto<ItensPedidoOutput>> GetList(int idPedido, PagedFilteredAndSortedInput input);
        Task<ItensPedidoOutput?> Get(int idPedido, int id);
        Task Update(int idPedido, int id, ItensPedidoInput input);
        Task Delete(int idPedido, int id);
    }
}
