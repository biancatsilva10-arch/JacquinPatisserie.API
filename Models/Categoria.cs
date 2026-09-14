using JacquinPatisserie.API.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JacquinPatisserie.API.Models;

[Table("Categoria")]
public class Categoria
{
    [Key]
    public Guid IdCategoria { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    public ICollection<Produto>? Produtos { get; set; }
}