using CadastroPedidos.Application.Dtos;
using CadastroPedidos.Domain.Entities;
using CadastroPedidos.Domain.Utils.Dependencies;
using CadastroPedidos.Domain.Utils.Repositories;
using CadastroPedidos.Domain.Utils.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CadastroPedidos.Application.Services
{
    public class PedidoService : IPedidoService, ITransientDependency
    {
        public readonly IRepository<Pedido> _pedidos;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoService(IRepository<Pedido> pedidos, IUnitOfWork unitOfWork)
        {
            _pedidos = pedidos;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(PedidoInput input)
        {
            var pedidoParaCriar = new Pedido(input.NomeCliente, input.EmailCliente, input.Pago);

            using (_unitOfWork.Begin())
            {
                await _pedidos.InsertAsync(pedidoParaCriar);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task Delete(int id)
        {
            var pedidoParaDeletar = await _pedidos.GetByIdAsync(id);

            using (_unitOfWork.Begin())
            {
                _pedidos.Delete(pedidoParaDeletar);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<PedidoOutput> Get(int id)
        {
            var pedido = await _pedidos.AsQueryable()
                .Include(pedido => pedido.ItensPedido)
                .ThenInclude(item => item.Produto)
            .FirstOrDefaultAsync(pedido => pedido.Id == id);

            var output = new PedidoOutput(pedido);
            return output;
        }

        public async Task<PagedResultDto<PedidoOutput>> GetList(PagedFilteredAndSortedInput input)
        {
            var pedidos = _pedidos.AsQueryable()
                .Include(pedido => pedido.ItensPedido)
                .ThenInclude(item => item.Produto);

            var itens = await pedidos
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Select(pedido => new PedidoOutput(pedido))
                .ToListAsync();
            var totalCount = await pedidos.CountAsync();

            var output = new PagedResultDto<PedidoOutput>(itens, totalCount);
            return output;
        }

        public async Task Update(int id, PedidoInput input)
        {
            var pedidoParaAtualizar = await _pedidos.GetByIdAsync(id);
            pedidoParaAtualizar.Atualizar(input.NomeCliente, input.EmailCliente, input.Pago);

            using (_unitOfWork.Begin())
            {
                _pedidos.Update(pedidoParaAtualizar);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
