using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JacquinPatisserie.API.Models;

[Table("TipoUsuario")]
public class TipoUsuario
{
    [Key]
    public Guid IdTipoUsuario { get; set; }

    [Required]
    [StringLength(50)]
    public string Titulo { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Usuario>? Usuarios { get; set; }
}