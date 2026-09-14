using System.ComponentModel.DataAnnotations;

namespace JacquinPatisserie.API.DTOs;

public class AtualizarUsuarioDto
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    public bool Situacao { get; set; } = true;
}