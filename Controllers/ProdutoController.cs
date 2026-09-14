using JacquinPatisserie.API.Context;
using JacquinPatisserie.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JacquinPatisserie.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> BuscarTodos([FromQuery] string? nome, [FromQuery] Guid? idCategoria)
    {
        var query = _context.Produtos
            .Include(p => p.Categoria)
            .Where(p => p.Situacao)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
        {
            query = query.Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
        }

        if (idCategoria.HasValue)
        {
            query = query.Where(p => p.IdCategoria == idCategoria.Value);
        }

        var listaProdutos = await query.ToListAsync();

        var resultado = listaProdutos.Select(p => new
        {
            p.IdProduto,
            p.Nome,
            p.Preco,
            p.ImagemUrl,
            p.DescricaoCurta,
            p.QuantidadeEstoque,
            Categoria = p.Categoria != null ? p.Categoria.Nome : null
        });

        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.IdProduto == id && p.Situacao);

        if (produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado." });
        }

        var resultado = new
        {
            produto.IdProduto,
            produto.Nome,
            produto.Preco,
            produto.ImagemUrl,
            produto.DescricaoCurta,
            produto.QuantidadeEstoque,
            Categoria = produto.Categoria != null ? produto.Categoria.Nome : null
        };

        return Ok(resultado);
    }


    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] Produto produto)
    {
        // Define a chave primária e o estado inicial ativo
        produto.IdProduto = Guid.NewGuid();
        produto.Situacao = true;

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarPorId), new { id = produto.IdProduto }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] Produto produtoAtualizado)
    {
        var produtoExistente = await _context.Produtos.FirstOrDefaultAsync(p => p.IdProduto == id && p.Situacao);

        if (produtoExistente == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado." });
        }

        produtoExistente.Nome = produtoAtualizado.Nome;
        produtoExistente.DescricaoCurta = produtoAtualizado.DescricaoCurta;
        produtoExistente.DescricaoLonga = produtoAtualizado.DescricaoLonga;
        produtoExistente.Preco = produtoAtualizado.Preco;
        produtoExistente.QuantidadeEstoque = produtoAtualizado.QuantidadeEstoque;
        produtoExistente.ImagemUrl = produtoAtualizado.ImagemUrl;
        produtoExistente.IdCategoria = produtoAtualizado.IdCategoria;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.IdProduto == id && p.Situacao);

        if (produto == null)
        {
            return NotFound(new { mensagem = "Produto não encontrado ou já inativado." });
        }

        // Inativação lógica
        produto.Situacao = false;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}