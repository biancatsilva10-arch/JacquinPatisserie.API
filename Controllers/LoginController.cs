using JacquinPatisserie.API.Context;
using JacquinPatisserie.API.DTOs;
using JacquinPatisserie.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JacquinPatisserie.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly AppDbContext _context;

    public LoginController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistroClienteDto dto)
    {
        var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (usuarioExistente != null)
        {
            return BadRequest(new { mensagem = "E-mail já cadastrado." });
        }

        var tipoCliente = await _context.TipoUsuarios.FirstOrDefaultAsync(t => t.Titulo == "Cliente");
        if (tipoCliente == null)
        {
            return BadRequest(new { mensagem = "Tipo de usuário 'Cliente' não encontrado no banco." });
        }

        var novoUsuario = new Usuario
        {
            IdUsuario = Guid.NewGuid(),
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha,
            IdTipoUsuario = tipoCliente.IdTipoUsuario,
            Situacao = true
        };

        _context.Usuarios.Add(novoUsuario);
        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "Usuário cadastrado com sucesso!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.TipoUsuario)
            .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Senha == dto.Senha && u.Situacao);

        if (usuario == null)
        {
            return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });
        }

        return Ok(new
        {
            usuario.IdUsuario,
            usuario.Nome,
            usuario.Email,
            TipoUsuario = usuario.TipoUsuario != null ? usuario.TipoUsuario.Titulo : null
        });
    }
}