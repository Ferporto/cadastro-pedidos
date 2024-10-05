using CadastroPedidos.Application.Dtos;
using CadastroPedidos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPedidos.API.Controllers;

[ApiController]
[Route("produtos")]
public class ProdutoController : ControllerBase
{
    public readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoOutput>> Create([FromBody] ProdutoInput input)
    {
        if (input == null)
        {
            return BadRequest();
        }
        
        var output = await _produtoService.Create(input);
        return CreatedAtAction(nameof(Get), new { id = output.Id }, output);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<ProdutoOutput>>> GetList([FromQuery] PagedInput input)
    {
        var output = await _produtoService.GetList(input);
        return Ok(output);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoOutput>> Get([FromRoute] int id)
    {
        var output = await _produtoService.Get(id);
        if (output == null) 
        {
            return NotFound();
        }

        return Ok(output);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProdutoInput input)
    {
        if (input == null)
        {
            return BadRequest();
        }

        await _produtoService.Update(id, input);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _produtoService.Delete(id);
        return NoContent();
    }
}