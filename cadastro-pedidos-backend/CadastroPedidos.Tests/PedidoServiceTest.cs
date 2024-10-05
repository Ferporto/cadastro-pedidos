using System.Net;
using CadastroPedidos.API.Controllers;
using CadastroPedidos.Application.Dtos;
using CadastroPedidos.Application.Services;
using CadastroPedidos.Domain.Entities;
using CadastroPedidos.Domain.Utils.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace TruckRegistration.Test;

public class PedidoServiceTest
{
    private readonly Mock<DbSet<Pedido>> _mockPedidosDbSet;
    private readonly Mock<DbContext> _mockDbContext;

    public PedidoServiceTest()
    {
        _mockPedidosDbSet = new Mock<DbSet<Pedido>>();
        _mockDbContext = new Mock<DbContext>();

        _mockDbContext.Setup(m => m.Set<Pedido>()).Returns(_mockPedidosDbSet.Object);
    }


    //[Fact]
    //public async Task Get()
    //{
    //    // Arrange  
    //    var pedidoServiceMock = new Mock<IPedidoService>();
    //
    //    var idPedido = 1;
    //
    //    var pedidoRetornado = new PedidoOutput
    //    {
    //        Id = idPedido,
    //        NomeCliente = "Cliente 01",
    //        EmailCliente = "email@cliente.com",
    //        Pago = true,
    //    };
    //
    //    pedidoServiceMock.Setup(service => service.Get(idPedido))
    //        .Returns(Task.FromResult(pedidoRetornado));
    //
    //
    //    var controller = new PedidoController(pedidoServiceMock.Object);
    //
    //    // Act
    //    var output = await controller.Get(idPedido);
    //
    //    // Assert
    //    var outputEsperado = new PedidoOutput
    //    {
    //        Id = idPedido,
    //        NomeCliente = "Cliente 01",
    //        EmailCliente = "email@cliente.com",
    //        Pago = true,
    //    };
    //
    //    var okResult = output.Result as OkObjectResult;
    //    Assert.NotNull(okResult);
    //    Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);
    //
    //    var okResultValue = okResult.Value as PedidoOutput;
    //    Assert.Equivalent(outputEsperado, okResultValue);
    //
    //    pedidoServiceMock.Verify(service => service.Get(idPedido), Times.Once);
    //}
    //
    //[Fact]
    //public async Task GetNotFound()
    //{
    //    // Arrange  
    //    var pedidoServiceMock = new Mock<IPedidoService>();
    //
    //    var idPedido = 1;
    //
    //    var pedidoRetornado = new PedidoOutput
    //    {
    //        Id = idPedido,
    //        NomeCliente = "Cliente 01",
    //        EmailCliente = "email@cliente.com",
    //        Pago = true,
    //    };
    //
    //    var controller = new PedidoController(pedidoServiceMock.Object);
    //
    //    // Act
    //    var output = await controller.Get(idPedido);
    //
    //    // Assert
    //    var notFoundResult = output.Result as NotFoundResult;
    //    Assert.NotNull(notFoundResult);
    //    Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);
    //
    //    pedidoServiceMock.Verify(service => service.Get(idPedido), Times.Once);
    //}
    //
    //[Fact]
    //public async Task GetList()
    //{
    //    // Arrange  
    //    var pedidoServiceMock = new Mock<IPedidoService>();
    //    var pedidoRepositoryMock = new Mock<IRepository<Pedido>>();
    //
    //    var idPedido = 1;
    //
    //    var input = new PagedInput
    //    {
    //        SkipCount = 0,
    //        MaxResultCount = 10
    //    };
    //
    //    var pedidosRetornado = new List<Pedido>
    //    {
    //        new()
    //        {
    //            Id = idPedido,
    //            NomeCliente = "Cliente 01",
    //            EmailCliente = "email@cliente.com",
    //            Pago = true,
    //            ItensPedido = new List<ItensPedido>
    //            {
    //                new()
    //                {
    //                    Id = 1,
    //                    IdPedido = idPedido,
    //                    IdProduto = 1,
    //                    Quantidade = 20,
    //                    Produto = new Produto
    //                    {
    //                        Id = 1,
    //                        NomeProduto = "Produto 01",
    //                        Valor = 20.50m,
    //                    }
    //                },
    //                new()
    //                {
    //                    Id = 2,
    //                    IdPedido = idPedido,
    //                    IdProduto = 2,
    //                    Quantidade = 15,
    //                    Produto = new Produto
    //                    {
    //                        Id = 2,
    //                        NomeProduto = "Produto 02",
    //                        Valor = 10,
    //                    }
    //                }
    //            }
    //        },
    //        new()
    //        {
    //            Id = idPedido,
    //            NomeCliente = "Cliente 02",
    //            EmailCliente = "email2@cliente.com",
    //            Pago = false,
    //            ItensPedido = new List<ItensPedido>()
    //        }
    //    };
    //
    //    var x = new List<PedidoOutput>
    //    {
    //        new()
    //        {
    //            Id = idPedido,
    //            NomeCliente = "Cliente 01",
    //            EmailCliente = "email@cliente.com",
    //            Pago = true,
    //            ItensPedido = new List<ItensPedidoOutput>
    //            {
    //                new()
    //                {
    //                    Id = 1,
    //                    IdPedido = idPedido,
    //                    IdProduto = 1,
    //                    NomeProduto = "Produto 01",
    //                    ValorUnitario = 20.50m,
    //                    Quantidade = 20,
    //                },
    //                new()
    //                {
    //                    Id = 2,
    //                    IdPedido = idPedido,
    //                    IdProduto = 2,
    //                    NomeProduto = "Produto 02",
    //                    ValorUnitario = 10,
    //                    Quantidade = 15,
    //                }
    //            }
    //        },
    //        new()
    //        {
    //            Id = idPedido,
    //            NomeCliente = "Cliente 02",
    //            EmailCliente = "email2@cliente.com",
    //            Pago = false,
    //            ItensPedido = new List<ItensPedidoOutput>()
    //        }
    //    };
    //
    //
    //    pedidoRepositoryMock.Setup(repository => repository.AsQueryable().Include(pedido => pedido.ItensPedido).ThenInclude(item => item.Produto))
    //        .Returns((Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Pedido, Produto>)pedidosRetornado.AsQueryable());
    //
    //    pedidoRepositoryMock.Setup(repository => repository.AsQueryable()
    //            .Include(pedido => pedido.ItensPedido)
    //            .ThenInclude(item => item.Produto)
    //            .Skip(0)
    //            .Take(25)
    //            .Select(pedido => new PedidoOutput(pedido))
    //            .ToListAsync())
    //        .Returns();
    //
    //    var controller = new PedidoController(pedidoServiceMock.Object);
    //
    //    // Act
    //    var output = await controller.GetList(input);
    //
    //    // Assert
    //    var outputEsperado = new PagedResultDto<PedidoOutput>
    //    {
    //        TotalCount = 2,
    //        Itens = new List<PedidoOutput>
    //        {
    //            new()
    //            {
    //                Id = idPedido,
    //                NomeCliente = "Cliente 01",
    //                EmailCliente = "email@cliente.com",
    //                Pago = true,
    //                ItensPedido = new List<ItensPedidoOutput>
    //                {
    //                    new()
    //                    {
    //                        Id = 1,
    //                        IdPedido = idPedido,
    //                        IdProduto = 1,
    //                        NomeProduto = "Produto 01",
    //                        ValorUnitario = 20.50m,
    //                        Quantidade = 20,
    //                    },
    //                    new()
    //                    {
    //                        Id = 2,
    //                        IdPedido = idPedido,
    //                        IdProduto = 2,
    //                        NomeProduto = "Produto 02",
    //                        ValorUnitario = 10,
    //                        Quantidade = 15,
    //                    }
    //                }
    //            },
    //            new()
    //            {
    //                Id = idPedido,
    //                NomeCliente = "Cliente 02",
    //                EmailCliente = "email2@cliente.com",
    //                Pago = false,
    //                ItensPedido = new List<ItensPedidoOutput>()
    //            }
    //        }
    //    };
    //
    //    var okResult = output.Result as OkObjectResult;
    //    Assert.NotNull(okResult);
    //    Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);
    //
    //    var okResultValue = okResult.Value as PagedResultDto<PedidoOutput>;
    //    Assert.Equivalent(outputEsperado, okResultValue);
    //
    //    pedidoServiceMock.Verify(service => service.GetList(input), Times.Once);
    //}

    //[Fact]
    //public async Task Create()
    //{
    //    // Arrange  
    //    var mockedTruckRepository = new Mock<ITruckRepository>();
    //    var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

    //    var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
    //    var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

    //    var input = new TruckInput
    //    {
    //        Id = truckId,
    //        LicensePlate = "Plate 01",
    //        ModelId = modelId
    //    };

    //    var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

    //    // Act
    //    var output = await controller.Create(input);

    //    // Assert
    //    var okResult = output as OkResult;
    //    Assert.NotNull(okResult);
    //    Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

    //    mockedTruckRepository.Verify(repository => repository.CreateAsync(
    //        It.Is<Truck>(truck => truck.Id == input.Id && truck.LicensePlate == input.LicensePlate &&
    //                              truck.ModelId == input.ModelId)
    //    ), Times.Once);
    //}

    //[Fact]
    //public async Task Update()
    //{
    //    // Arrange  
    //    var mockedTruckRepository = new Mock<ITruckRepository>();
    //    var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

    //    var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
    //    var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

    //    var input = new TruckInput
    //    {
    //        Id = truckId,
    //        LicensePlate = "Plate 01",
    //        ModelId = modelId
    //    };

    //    var expectedTruck = new Truck
    //    {
    //        Id = truckId,
    //        LicensePlate = "Plate 01",
    //        ManufacturingYear = 2022,
    //        ModelId = modelId
    //    };

    //    mockedTruckRepository.Setup(repository => repository.FirstOrDefaultAsync(truck => truck.Id == truckId))
    //        .Returns(Task.FromResult(expectedTruck)!);

    //    var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

    //    // Act
    //    var output = await controller.Update(truckId, input);

    //    // Assert
    //    var okResult = output as OkResult;
    //    Assert.NotNull(okResult);
    //    Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

    //    mockedTruckRepository.Verify(repository => repository.UpdateAsync(
    //        It.Is<Truck>(truck => truck.Id == input.Id && truck.LicensePlate == input.LicensePlate &&
    //                              truck.ModelId == input.ModelId)
    //    ), Times.Once);
    //}

    //[Fact]
    //public async Task UpdateNotFound()
    //{
    //    // Arrange  
    //    var mockedTruckRepository = new Mock<ITruckRepository>();
    //    var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

    //    var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
    //    var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

    //    var input = new TruckInput
    //    {
    //        Id = truckId,
    //        LicensePlate = "Plate 01",
    //        ModelId = modelId
    //    };

    //    var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

    //    // Act
    //    var output = await controller.Update(truckId, input);

    //    // Assert
    //    var notFoundResult = output as NotFoundResult;
    //    Assert.NotNull(notFoundResult);
    //    Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);

    //    mockedTruckRepository.Verify(repository => repository.UpdateAsync(
    //        It.Is<Truck>(truck => truck.Id == input.Id && truck.LicensePlate == input.LicensePlate &&
    //                              truck.ModelId == input.ModelId)
    //    ), Times.Never);
    //}

    //[Fact]
    //public async Task Delete()
    //{
    //    // Arrange  
    //    var mockedTruckRepository = new Mock<ITruckRepository>();
    //    var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

    //    var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
    //    var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

    //    var expectedTruck = new Truck
    //    {
    //        Id = truckId,
    //        LicensePlate = "Plate 01",
    //        ManufacturingYear = 2022,
    //        ModelId = modelId
    //    };

    //    mockedTruckRepository.Setup(repository => repository.FirstOrDefaultAsync(truck => truck.Id == truckId))
    //        .Returns(Task.FromResult(expectedTruck)!);

    //    var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

    //    // Act
    //    var output = await controller.Delete(truckId);

    //    // Assert
    //    var okResult = output as OkResult;
    //    Assert.NotNull(okResult);
    //    Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

    //    mockedTruckRepository.Verify(repository => repository.DeleteAsync(
    //        It.Is<Truck>(truck => truck.Id == expectedTruck.Id && truck.LicensePlate == expectedTruck.LicensePlate &&
    //                              truck.ModelId == expectedTruck.ModelId)
    //    ), Times.Once);
    //}

    //[Fact]
    //public async Task DeleteNotFound()
    //{
    //    // Arrange  
    //    var mockedTruckRepository = new Mock<ITruckRepository>();
    //    var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

    //    var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");

    //    var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

    //    // Act
    //    var output = await controller.Delete(truckId);

    //    // Assert
    //    var notFoundResult = output as NotFoundResult;
    //    Assert.NotNull(notFoundResult);
    //    Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);

    //    mockedTruckRepository.Verify(repository => repository.DeleteAsync(
    //        It.Is<Truck>(truck => truck.Id == truckId)
    //    ), Times.Never);
    //}
}