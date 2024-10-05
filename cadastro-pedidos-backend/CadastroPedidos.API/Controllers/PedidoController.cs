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
    public async Task<ActionResult<PedidoOutput>> Create([FromBody] PedidoInput input)
    {
        if (input == null)
        {
            return BadRequest();
        }

        var output = await _pedidoService.Create(input);
        return CreatedAtAction(nameof(Get), new { id = output.Id }, output);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<PedidoOutput>>> GetList([FromQuery] PagedInput input)
    {
        var output = await _pedidoService.GetList(input);
        return Ok(output);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoOutput>> Get([FromRoute] int id)
    {
        var output = await _pedidoService.Get(id);
        if (output == null) 
        {
            return NotFound();
        }

        return Ok(output);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PedidoInput input)
    {
        if (input == null)
        {
            return BadRequest();
        }

        await _pedidoService.Update(id, input);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _pedidoService.Delete(id);
        return NoContent();
    }
}