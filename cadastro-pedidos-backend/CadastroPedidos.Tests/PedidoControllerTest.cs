using System.Net;
using CadastroPedidos.API.Controllers;
using CadastroPedidos.Application.Dtos;
using CadastroPedidos.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace CadastroPedidos.Tests;

public class PedidoControllerTest
{
    [Fact]
    public async Task Get()
    {
        // Arrange  
        var pedidoServiceMock = new Mock<IPedidoService>();

        var idPedido = 1;

        var pedidoRetornado = new PedidoOutput
        {
            Id = idPedido,
            NomeCliente = "Cliente 01",
            EmailCliente = "email@cliente.com",
            Pago = true,
        };

        pedidoServiceMock.Setup(service => service.Get(idPedido))
            .Returns(Task.FromResult(pedidoRetornado));


        var controller = new PedidoController(pedidoServiceMock.Object);

        // Act
        var output = await controller.Get(idPedido);

        // Assert
        var outputEsperado = new PedidoOutput
        {
            Id = idPedido,
            NomeCliente = "Cliente 01",
            EmailCliente = "email@cliente.com",
            Pago = true,
        };

        var okResult = output.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        var okResultValue = okResult.Value as PedidoOutput;
        Assert.Equivalent(outputEsperado, okResultValue);

        pedidoServiceMock.Verify(service => service.Get(idPedido), Times.Once);
    }

    [Fact]
    public async Task GetNotFound()
    {
        // Arrange  
        var pedidoServiceMock = new Mock<IPedidoService>();

        var idPedido = 1;

        var pedidoRetornado = new PedidoOutput
        {
            Id = idPedido,
            NomeCliente = "Cliente 01",
            EmailCliente = "email@cliente.com",
            Pago = true,
        };

        var controller = new PedidoController(pedidoServiceMock.Object);

        // Act
        var output = await controller.Get(idPedido);

        // Assert
        var notFoundResult = output.Result as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);

        pedidoServiceMock.Verify(service => service.Get(idPedido), Times.Once);
    }

    [Fact]
    public async Task GetList()
    {
        // Arrange  
        var pedidoServiceMock = new Mock<IPedidoService>();

        var idPedido = 1;

        var input = new PagedInput
        {
            SkipCount = 0,
            MaxResultCount = 10
        };

        var pedidosRetornado = new PagedResultDto<PedidoOutput>
        {
            TotalCount = 2,
            Itens = new List<PedidoOutput>
            {
                new()
                {
                    Id = idPedido,
                    NomeCliente = "Cliente 01",
                    EmailCliente = "email@cliente.com",
                    Pago = true,
                    ItensPedido = new List<ItensPedidoOutput>
                    {
                        new()
                        {
                            Id = 1,
                            IdPedido = idPedido,
                            IdProduto = 1,
                            NomeProduto = "Produto 01",
                            ValorUnitario = 20.50m,
                            Quantidade = 20,
                        },
                        new()
                        {
                            Id = 2,
                            IdPedido = idPedido,
                            IdProduto = 2,
                            NomeProduto = "Produto 02",
                            ValorUnitario = 10,
                            Quantidade = 15,
                        }
                    }
                },
                new()
                {
                    Id = idPedido,
                    NomeCliente = "Cliente 02",
                    EmailCliente = "email2@cliente.com",
                    Pago = false,
                    ItensPedido = new List<ItensPedidoOutput>()
                }
            }
        };

        pedidoServiceMock.Setup(service => service.GetList(input))
            .Returns(Task.FromResult(pedidosRetornado));


        var controller = new PedidoController(pedidoServiceMock.Object);

        // Act
        var output = await controller.GetList(input);

        // Assert
        var outputEsperado = new PagedResultDto<PedidoOutput>
        {
            TotalCount = 2,
            Itens = new List<PedidoOutput>
            {
                new()
                {
                    Id = idPedido,
                    NomeCliente = "Cliente 01",
                    EmailCliente = "email@cliente.com",
                    Pago = true,
                    ItensPedido = new List<ItensPedidoOutput>
                    {
                        new()
                        {
                            Id = 1,
                            IdPedido = idPedido,
                            IdProduto = 1,
                            NomeProduto = "Produto 01",
                            ValorUnitario = 20.50m,
                            Quantidade = 20,
                        },
                        new()
                        {
                            Id = 2,
                            IdPedido = idPedido,
                            IdProduto = 2,
                            NomeProduto = "Produto 02",
                            ValorUnitario = 10,
                            Quantidade = 15,
                        }
                    }
                },
                new()
                {
                    Id = idPedido,
                    NomeCliente = "Cliente 02",
                    EmailCliente = "email2@cliente.com",
                    Pago = false,
                    ItensPedido = new List<ItensPedidoOutput>()
                }
            }
        };

        var okResult = output.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        var okResultValue = okResult.Value as PagedResultDto<PedidoOutput>;
        Assert.Equivalent(outputEsperado, okResultValue);

        pedidoServiceMock.Verify(service => service.GetList(input), Times.Once);
    }

    [Fact]
    public async Task Create()
    {
        // Arrange  
        var pedidoServiceMock = new Mock<IPedidoService>();
        var input = new PedidoInput
        {
            NomeCliente = "Cliente 01",
            EmailCliente = "email@cliente.com",
            Pago = false
        };

        var pedidoRetornado = new PedidoOutput
        {
            Id = 1,
            NomeCliente = "Cliente 01",
            EmailCliente = "email@cliente.com",
            Pago = false,
            ItensPedido = new List<ItensPedidoOutput>()
        };

        pedidoServiceMock.Setup(service => service.Create(input))
            .ReturnsAsync(pedidoRetornado);

        var controller = new PedidoController(pedidoServiceMock.Object);

        // Act
        var result = await controller.Create(input);

        // Assert
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(createdResult);
        Assert.Equal((int)HttpStatusCode.Created, createdResult.StatusCode);

        var outputValue = createdResult.Value as PedidoOutput;
        Assert.NotNull(outputValue);
        Assert.Equal(pedidoRetornado.Id, outputValue.Id);
        Assert.Equal(pedidoRetornado.NomeCliente, outputValue.NomeCliente);
        Assert.Equal(pedidoRetornado.EmailCliente, outputValue.EmailCliente);

        pedidoServiceMock.Verify(service => service.Create(input), Times.Once);
    }

    [Fact]
    public async Task CreateBadRequest()
    {
        // Arrange  
        var pedidoServiceMock = new Mock<IPedidoService>();
        var controller = new PedidoController(pedidoServiceMock.Object);

        // Act
        var result = await controller.Create(null);

        // Assert
        var badRequestResult = result.Result as BadRequestResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task Update()
    {
        // Arrange  
        var pedidoServiceMock = new Mock<IPedidoService>();
        var idPedido = 1;

        var input = new PedidoInput
        {
            NomeCliente = "Cliente Atualizado",
            EmailCliente = "email@cliente.com"
        };

        pedidoServiceMock.Setup(service => service.Update(idPedido, input))
            .Returns(Task.CompletedTask);

        var controller = new PedidoController(pedidoServiceMock.Object);

        // Act
        var result = await controller.Update(idPedido, input);

        // Assert
        var noContentResult = result as NoContentResult;
        Assert.NotNull(noContentResult);
        Assert.Equal((int)HttpStatusCode.NoContent, noContentResult.StatusCode);

        pedidoServiceMock.Verify(service => service.Update(idPedido, input), Times.Once);
    }

    [Fact]
    public async Task UpdateBadRequest()
    {
        // Arrange  
        var pedidoServiceMock = new Mock<IPedidoService>();
        var idPedido = 1;
        var controller = new PedidoController(pedidoServiceMock.Object);

        // Act
        var result = await controller.Update(idPedido, null);

        // Assert
        var badRequestResult = result as BadRequestResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task Delete()
    {
        // Arrange  
        var pedidoServiceMock = new Mock<IPedidoService>();
        var idPedido = 1;

        pedidoServiceMock.Setup(service => service.Delete(idPedido))
            .Returns(Task.CompletedTask);

        var controller = new PedidoController(pedidoServiceMock.Object);

        // Act
        var result = await controller.Delete(idPedido);

        // Assert
        var noContentResult = result as NoContentResult;
        Assert.NotNull(noContentResult);
        Assert.Equal((int)HttpStatusCode.NoContent, noContentResult.StatusCode);

        pedidoServiceMock.Verify(service => service.Delete(idPedido), Times.Once);
    }
}
