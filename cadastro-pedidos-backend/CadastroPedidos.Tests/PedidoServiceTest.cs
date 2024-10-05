using CadastroPedidos.Application.Dtos;
using CadastroPedidos.Application.Services;
using CadastroPedidos.Domain.Entities;
using CadastroPedidos.Domain.Utils.Repositories;
using CadastroPedidos.Domain.Utils.UnitOfWork;
using CadastroPedidos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CadastroPedidos.Tests
{
    public class PedidoServiceTest : IDisposable
    {
        private readonly PedidoService _pedidoService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CadastroPedidosDbContext _dbContext;

        public PedidoServiceTest()
        {
            var options = new DbContextOptionsBuilder<CadastroPedidosDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new CadastroPedidosDbContext(options);
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            var repository = new Repository<Pedido>(_dbContext);
            _pedidoService = new PedidoService(repository, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Create()
        {
            // Arrange
            var input = new PedidoInput { NomeCliente = "Cliente 01", EmailCliente = "email@cliente.com", Pago = true };

            // Act
            var result = await _pedidoService.Create(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Cliente 01", result.NomeCliente);
            Assert.Equal("email@cliente.com", result.EmailCliente);
            Assert.True(result.Pago);
        }

        [Fact]
        public async Task Get()
        {
            // Arrange
            var pedido = new Pedido("Cliente 01", "email@cliente.com", true)
            {
                Id = 1,
                ItensPedido = new List<ItensPedido>
        {
            new()
            {
                Id = 1,
                IdPedido = 1,
                IdProduto = 1,
                Quantidade = 20,
                Produto = new Produto
                {
                    NomeProduto = "Produto 01",
                    Valor = 20.50m,
                }
            },
            new()
            {
                Id = 2,
                IdPedido = 1,
                IdProduto = 2,
                Quantidade = 15,
                Produto = new Produto
                {
                    NomeProduto = "Produto 02",
                    Valor = 10.00m,
                }
            }
        }
            };

            _dbContext.Pedido.Add(pedido);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _pedidoService.Get(pedido.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pedido.Id, result.Id);
            Assert.Equal("Cliente 01", result.NomeCliente);
            Assert.Equal("email@cliente.com", result.EmailCliente);
            Assert.True(result.Pago);
            Assert.NotNull(result.ItensPedido);
            Assert.Equal(pedido.ItensPedido.Count, result.ItensPedido.Count);

            var itensPedidoResult = result.ItensPedido.ToList();
            for (int i = 0; i < pedido.ItensPedido.Count; i++)
            {
                Assert.Equal(pedido.ItensPedido.ElementAt(i).Produto.NomeProduto, itensPedidoResult[i].NomeProduto);
                Assert.Equal(pedido.ItensPedido.ElementAt(i).Produto.Valor, itensPedidoResult[i].ValorUnitario);
                Assert.Equal(pedido.ItensPedido.ElementAt(i).Quantidade, itensPedidoResult[i].Quantidade);
            }
        }

        [Fact]
        public async Task GetNull()
        {
            // Act
            var result = await _pedidoService.Get(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetList_ReturnsPagedResultDto()
        {
            // Arrange
            var pedidos = new List<Pedido>
    {
        new Pedido("Cliente 01", "email@cliente.com", true)
        {
            Id = 1,
            ItensPedido = new List<ItensPedido>
            {
                new()
                {
                    Id = 1,
                    IdPedido = 1,
                    IdProduto = 1,
                    Quantidade = 20,
                    Produto = new Produto
                    {
                        NomeProduto = "Produto 01",
                        Valor = 20.50m,
                    }
                },
                new()
                {
                    Id = 2,
                    IdPedido = 1,
                    IdProduto = 2,
                    Quantidade = 15,
                    Produto = new Produto
                    {
                        NomeProduto = "Produto 02",
                        Valor = 10.00m,
                    }
                }
            }
        },
        new Pedido("Cliente 02", "email2@cliente.com", false)
        {
            Id = 2,
            ItensPedido = new List<ItensPedido>()
        }
    };

            _dbContext.Pedido.AddRange(pedidos);
            await _dbContext.SaveChangesAsync();

            var input = new PagedInput { SkipCount = 0, MaxResultCount = 10 };

            // Act
            var result = await _pedidoService.GetList(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(2, result.Itens.Count);

            var primeiroPedido = result.Itens[0];
            Assert.Equal(1, primeiroPedido.Id);
            Assert.Equal("Cliente 01", primeiroPedido.NomeCliente);
            Assert.Equal("email@cliente.com", primeiroPedido.EmailCliente);
            Assert.True(primeiroPedido.Pago);
            Assert.Equal(2, primeiroPedido.ItensPedido.Count);

            var primeiroPedidoItens = primeiroPedido.ItensPedido.ToList();
            Assert.Equal(2, primeiroPedidoItens.Count);
            Assert.Equal("Produto 01", primeiroPedidoItens[0].NomeProduto);
            Assert.Equal(20.50m, primeiroPedidoItens[0].ValorUnitario);
            Assert.Equal(20, primeiroPedidoItens[0].Quantidade);
            Assert.Equal("Produto 02", primeiroPedidoItens[1].NomeProduto);
            Assert.Equal(10.00m, primeiroPedidoItens[1].ValorUnitario);
            Assert.Equal(15, primeiroPedidoItens[1].Quantidade);

            var segundoPedido = result.Itens[1];
            Assert.Equal(2, segundoPedido.Id);
            Assert.Equal("Cliente 02", segundoPedido.NomeCliente);
            Assert.Equal("email2@cliente.com", segundoPedido.EmailCliente);
            Assert.False(segundoPedido.Pago);
            Assert.Empty(segundoPedido.ItensPedido);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var pedido = new Pedido("Cliente 01", "email@cliente.com", true)
            {
                Id = 1
            };
            _dbContext.Pedido.Add(pedido);
            await _dbContext.SaveChangesAsync();

            var input = new PedidoInput { NomeCliente = "Cliente Atualizado", EmailCliente = "email@atualizado.com", Pago = false };

            // Act
            await _pedidoService.Update(pedido.Id, input);

            // Assert
            var updatedPedido = await _dbContext.Pedido.FindAsync(pedido.Id);
            Assert.NotNull(updatedPedido);
            Assert.Equal("Cliente Atualizado", updatedPedido.NomeCliente);
            Assert.Equal("email@atualizado.com", updatedPedido.EmailCliente);
            Assert.False(updatedPedido.Pago);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
