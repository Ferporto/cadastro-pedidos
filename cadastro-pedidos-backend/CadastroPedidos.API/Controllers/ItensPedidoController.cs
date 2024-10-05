using CadastroPedidos.Application.Dtos;
using CadastroPedidos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPedidos.API.Controllers;

[ApiController]
[Route("pedidos/{idPedido}/itens")]
// TODO Ajeitar rota e colocar id do pedido nela
public class ItensPedidoController : ControllerBase
{
    public readonly IItensPedidoService _itensPedidoService;

    public ItensPedidoController(IItensPedidoService itensPedidoService)
    {
        _itensPedidoService = itensPedidoService;
    }

    [HttpPost]
    public async Task<ActionResult<ItensPedidoOutput>> Create([FromRoute] int idPedido, [FromBody] ItensPedidoInput input)
    {
        if (input == null)
        {
            return BadRequest();
        }

        var output = await _itensPedidoService.Create(idPedido, input);
        return CreatedAtAction(nameof(Get), new { id = output.Id, idPedido = output.IdPedido }, output);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<ItensPedidoOutput>>> GetList([FromRoute] int idPedido, [FromQuery] PagedFilteredAndSortedInput input)
    {
        var output = await _itensPedidoService.GetList(idPedido, input);
        return Ok(output);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItensPedidoOutput>> Get([FromRoute] int idPedido, [FromRoute] int id)
    {
        var output = await _itensPedidoService.Get(idPedido, id);
        if (output == null) 
        {
            return NotFound();
        }

        return Ok(output);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int idPedido, [FromRoute] int id, [FromBody] ItensPedidoInput input)
    {
        if (input == null)
        {
            return BadRequest();
        }

        await _itensPedidoService.Update(idPedido, id, input);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int idPedido, [FromRoute] int id)
    {
        await _itensPedidoService.Delete(idPedido, id);
        return NoContent();
    }
}