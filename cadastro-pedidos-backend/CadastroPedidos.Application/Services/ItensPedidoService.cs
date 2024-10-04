using CadastroPedidos.Application.Dtos;
using CadastroPedidos.Domain.Entities;
using CadastroPedidos.Domain.Utils.Repositories;
using CadastroPedidos.Domain.Utils.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CadastroPedidos.Application.Services
{
    public class ItensPedidoService : IItensPedidoService
    {
        public readonly IRepository<ItensPedido> _itensPedido;
        private readonly IUnitOfWork _unitOfWork;

        public ItensPedidoService(IRepository<ItensPedido> itensPedido, IUnitOfWork unitOfWork)
        {
            _itensPedido = itensPedido;
            _unitOfWork = unitOfWork;
        }

        public async Task<ItensPedidoOutput> Create(int idPedido, ItensPedidoInput input)
        {
            var itensPedidoParaCriar = new ItensPedido(idPedido, input.IdProduto, input.Quantidade);

            ItensPedido itensPedidoCriado;
            using (_unitOfWork.Begin())
            {
                itensPedidoCriado = await _itensPedido.InsertAsync(itensPedidoParaCriar);
                await _unitOfWork.CompleteAsync();
            }

            return new ItensPedidoOutput(itensPedidoCriado);
        }

        public async Task Delete(int idPedido, int id)
        {
            var itensPedidoParaDeletar = await _itensPedido.GetByIdAsync(id);

            using (_unitOfWork.Begin())
            {
                _itensPedido.Delete(itensPedidoParaDeletar);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<ItensPedidoOutput?> Get(int idPedido, int id)
        {
            var itensPedido = await _itensPedido.AsQueryable()
                .Include(item => item.Produto)
            .Where(item => item.IdPedido == idPedido)
            .FirstOrDefaultAsync(item => item.Id == id);
            if (itensPedido == null)
            {
                return null;
            }

            var output = new ItensPedidoOutput(itensPedido);
            return output;
        }

        public async Task<PagedResultDto<ItensPedidoOutput>> GetList(int idPedido, PagedFilteredAndSortedInput input)
        {
            var itensPedido = _itensPedido.AsQueryable()
                .Include(item => item.Produto)
                .Where(item => item.IdPedido == idPedido);

            var itens = await itensPedido
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Select(item => new ItensPedidoOutput(item))
                .ToListAsync();
            var totalCount = await itensPedido.CountAsync();

            var output = new PagedResultDto<ItensPedidoOutput>(itens, totalCount);
            return output;
        }

        public async Task Update(int idPedido, int id, ItensPedidoInput input)
        {
            var itensPedidoParaAtualizar = await _itensPedido.GetByIdAsync(id);
            itensPedidoParaAtualizar.Atualizar(idPedido, input.IdProduto, input.Quantidade);

            using (_unitOfWork.Begin())
            {
                _itensPedido.Update(itensPedidoParaAtualizar);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
