using JacquinPatisserie.API.Context;
using JacquinPatisserie.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JacquinPatisserie.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> BuscarTodos()
    {
        var categorias = await _context.Categorias.ToListAsync();
        return Ok(categorias);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] Categoria categoria)
    {
        categoria.IdCategoria = Guid.NewGuid();

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return Ok(categoria);
    }
}