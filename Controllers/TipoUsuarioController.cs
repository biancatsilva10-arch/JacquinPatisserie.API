using JacquinPatisserie.API.Context;
using JacquinPatisserie.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JacquinPatisserie.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoUsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public TipoUsuarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var tipos = await _context.TipoUsuarios.ToListAsync();
        return Ok(tipos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var tipo = await _context.TipoUsuarios.FindAsync(id);
        if (tipo == null) return NotFound(new { mensagem = "Tipo de usuário não encontrado." });

        return Ok(tipo);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] TipoUsuario tipoUsuario)
    {
        tipoUsuario.IdTipoUsuario = Guid.NewGuid();
        _context.TipoUsuarios.Add(tipoUsuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterPorId), new { id = tipoUsuario.IdTipoUsuario }, tipoUsuario);
    }
}