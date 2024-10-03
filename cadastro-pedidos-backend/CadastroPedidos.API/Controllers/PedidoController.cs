using CadastroPedidos.Application.Dtos;
using CadastroPedidos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPedidos.API.Controllers;

[ApiController]
[Route("pedidos")]
public class PedidoController : ControllerBase
{
    public readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PedidoInput input)
    {
        await _pedidoService.Create(input);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PagedFilteredAndSortedInput input)
    {
        var output = await _pedidoService.GetList(input);
        return Ok(output);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var output = await _pedidoService.Get(id);
        return Ok(output);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PedidoInput input)
    {
        await _pedidoService.Update(id, input);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _pedidoService.Delete(id);
        return Ok();
    }
}