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
        public async Task CreateValidInputReturnsPedidoOutput()
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
        public async Task GetValidIdReturnsPedidoOutput()
        {
            // Arrange
            var pedido = new Pedido("Cliente 01", "email@cliente.com", true);
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
        }

        [Fact]
        public async Task GetInvalidIdReturnsNull()
        {
            // Act
            var result = await _pedidoService.Get(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetListReturnsPagedResultDto()
        {
            // Arrange
            var pedidos = new List<Pedido>
            {
                new Pedido("Cliente 01", "email@cliente.com", true),
                new Pedido("Cliente 02", "email2@cliente.com", false)
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
        }

        [Fact]
        public async Task UpdateValidIdUpdatesPedido()
        {
            // Arrange
            var pedido = new Pedido("Cliente 01", "email@cliente.com", true);
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
