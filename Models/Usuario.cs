using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JacquinPatisserie.API.Models;

[Table("Usuario")]
public class Usuario
{
    [Key]
    public Guid IdUsuario { get; set; }

    [Required]
    public Guid IdTipoUsuario { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    // Caso no banco esteja como SenhaHash, SenhaUsuario, etc. Ajuste o texto dentro de [Column("...")]
    [Required]
    [StringLength(255)]
    [Column("SenhaHash")] // <--- TROQUE "SenhaHash" PELO NOME DA COLUNA NO SEU BANCO
    public string Senha { get; set; } = string.Empty;

    public bool Situacao { get; set; } = true;

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    [ForeignKey("IdTipoUsuario")]
    public TipoUsuario? TipoUsuario { get; set; }

    public ICollection<Avaliacao>? Avaliacoes { get; set; }
}