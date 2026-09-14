using JacquinPatisserie.API.Context;
using JacquinPatisserie.API.DTOs;
using JacquinPatisserie.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JacquinPatisserie.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.TipoUsuario)
            .Select(u => new
            {
                u.IdUsuario,
                u.Nome,
                u.Email,
                u.Situacao,
                TipoUsuario = u.TipoUsuario != null ? u.TipoUsuario.Titulo : null
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.TipoUsuario)
            .Where(u => u.IdUsuario == id)
            .Select(u => new
            {
                u.IdUsuario,
                u.Nome,
                u.Email,
                u.Situacao,
                TipoUsuario = u.TipoUsuario != null ? u.TipoUsuario.Titulo : null
            })
            .FirstOrDefaultAsync();

        if (usuario == null) return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(usuario);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound(new { mensagem = "Usuário não encontrado." });

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

   

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarUsuarioDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound(new { mensagem = "Usuário não encontrado." });

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.Situacao = dto.Situacao;

        await _context.SaveChangesAsync();
        return Ok(new { mensagem = "Usuário atualizado com sucesso!" });
    }
}