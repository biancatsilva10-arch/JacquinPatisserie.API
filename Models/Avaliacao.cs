using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JacquinPatisserie.API.Models;

[Table("Avaliacao")]
public class Avaliacao
{
    [Key]
    public Guid IdAvaliacao { get; set; }

    [Required]
    public Guid IdUsuario { get; set; }

    [Required]
    public Guid IdProduto { get; set; }

    [Required]
    public int Nota { get; set; }

    public string? Comentario { get; set; }

    // CASO 1: Se o nome no banco for "Data", adicione o atributo [Column("Data")]
    // [Column("Data")]
    // public DateTime DataAvaliacao { get; set; }

    // CASO 2: Se a coluna NÃO EXISTE na tabela do SQL Server, adicione [NotMapped]:
    [NotMapped]
    public DateTime DataAvaliacao { get; set; } = DateTime.Now;

    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }

    [ForeignKey("IdProduto")]
    public Produto? Produto { get; set; }
}