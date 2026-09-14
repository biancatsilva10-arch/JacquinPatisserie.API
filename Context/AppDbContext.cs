using JacquinPatisserie.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JacquinPatisserie.API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<TipoUsuario> TipoUsuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}