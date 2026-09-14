using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JacquinPatisserie.API.Models;

[Table("Produto")]
public class Produto
{
    [Key]
    public Guid IdProduto { get; set; }

    [Required]
    public Guid IdCategoria { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(500)]
    public string ImagemUrl { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string DescricaoCurta { get; set; } = string.Empty;

    [Required]
    public string DescricaoLonga { get; set; } = string.Empty;

    public int QuantidadeEstoque { get; set; } = 0;

    public bool Situacao { get; set; } = true;

    [ForeignKey("IdCategoria")]
    public Categoria? Categoria { get; set; }

    public ICollection<Avaliacao>? Avaliacoes { get; set; }
}