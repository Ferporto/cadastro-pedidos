using CadastroPedidos.Application.Dtos;
using CadastroPedidos.Domain.Entities;
using CadastroPedidos.Domain.Utils.Repositories;
using CadastroPedidos.Domain.Utils.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CadastroPedidos.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        public readonly IRepository<Produto> _produtos;
        private readonly IUnitOfWork _unitOfWork;

        public ProdutoService(IRepository<Produto> produtos, IUnitOfWork unitOfWork)
        {
            _produtos = produtos;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProdutoOutput> Create(ProdutoInput input)
        {
            var produtoParaCriar = new Produto(input.NomeProduto, input.Valor);

            Produto produtoCriado;
            using (_unitOfWork.Begin())
            {
                produtoCriado = await _produtos.InsertAsync(produtoParaCriar);
                await _unitOfWork.CompleteAsync();
            }

            return new ProdutoOutput(produtoCriado);
        }

        public async Task Delete(int id)
        {
            var produtoParaDeletar = await _produtos.GetByIdAsync(id);

            using (_unitOfWork.Begin())
            {
                _produtos.Delete(produtoParaDeletar);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<ProdutoOutput?> Get(int id)
        {
            var produto = await _produtos.GetByIdAsync(id);
            if (produto == null)
            {
                return null;
            }

            var output = new ProdutoOutput(produto);
            return output;
        }

        public async Task<PagedResultDto<ProdutoOutput>> GetList(PagedFilteredAndSortedInput input)
        {
            var produtos = _produtos.AsQueryable();

            var itens = await produtos
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Select(produto => new ProdutoOutput(produto))
                .ToListAsync();
            var totalCount = await produtos.CountAsync();

            var output = new PagedResultDto<ProdutoOutput>(itens, totalCount);
            return output;
        }

        public async Task Update(int id, ProdutoInput input)
        {
            var produtoParaAtualizar = await _produtos.GetByIdAsync(id);
            produtoParaAtualizar.Atualizar(input.NomeProduto, input.Valor);

            using (_unitOfWork.Begin())
            {
                _produtos.Update(produtoParaAtualizar);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
