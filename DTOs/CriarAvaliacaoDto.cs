using System.ComponentModel.DataAnnotations;

namespace JacquinPatisserie.API.DTOs;

public class CriarAvaliacaoDto
{
    [Required]
    public Guid IdUsuario { get; set; }

    [Required]
    public Guid IdProduto { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "A nota deve ser entre 1 e 5.")]
    public int Nota { get; set; }

    [StringLength(500)]
    public string? Comentario { get; set; }
}