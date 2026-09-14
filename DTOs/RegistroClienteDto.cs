using System.ComponentModel.DataAnnotations;

namespace JacquinPatisserie.API.DTOs;

public class RegistroClienteDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
    public string Senha { get; set; } = string.Empty;
}