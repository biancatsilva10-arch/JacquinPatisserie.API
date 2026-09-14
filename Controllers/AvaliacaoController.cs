using JacquinPatisserie.API.Context;
using JacquinPatisserie.API.DTOs;
using JacquinPatisserie.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JacquinPatisserie.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvaliacaoController : ControllerBase
{
    private readonly AppDbContext _context;

    public AvaliacaoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("produto/{idProduto:guid}")]
    public async Task<IActionResult> ObterPorProduto(Guid idProduto)
    {
        var avaliacoes = await _context.Avaliacoes
            .Include(a => a.Usuario)
            .Where(a => a.IdProduto == idProduto)
            .Select(a => new
            {
                a.IdAvaliacao,
                a.Nota,
                a.Comentario,
                Usuario = a.Usuario != null ? a.Usuario.Nome : "Anônimo"
            })
            .ToListAsync();

        return Ok(avaliacoes);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarAvaliacaoDto dto)
    {
        var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.IdUsuario == dto.IdUsuario);
        if (!usuarioExiste)
        {
            return BadRequest(new { mensagem = "Usuário não encontrado." });
        }

        var produtoExiste = await _context.Produtos.AnyAsync(p => p.IdProduto == dto.IdProduto);
        if (!produtoExiste)
        {
            return BadRequest(new { mensagem = "Produto não encontrado." });
        }

        var jaAvaliou = await _context.Avaliacoes
            .AnyAsync(a => a.IdUsuario == dto.IdUsuario && a.IdProduto == dto.IdProduto);

        if (jaAvaliou)
        {
            return BadRequest(new { mensagem = "Este usuário já avaliou este produto." });
        }

        var avaliacao = new Avaliacao
        {
            IdAvaliacao = Guid.NewGuid(),
            IdUsuario = dto.IdUsuario,
            IdProduto = dto.IdProduto,
            Nota = dto.Nota,
            Comentario = dto.Comentario
        };

        _context.Avaliacoes.Add(avaliacao);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterPorProduto), new { idProduto = dto.IdProduto }, avaliacao);
    }
}