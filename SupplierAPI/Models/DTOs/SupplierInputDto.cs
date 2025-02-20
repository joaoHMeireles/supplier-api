using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SupplierAPI.Models.DTOs;

public class SupplierInputDto
{
    [Required]
    [MaxLength(100)]
    public required string Nome { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Phone]
    [AllowNull]
    public string? Telefone { get; set; }

    [AllowNull]
    [MaxLength(150)]
    public string? RazaoSocial { get; set; }
    
    [Required]
    public long CEP { get; set; }

    [Required]
    [StringLength(14, MinimumLength = 14)]
    public required string CNPJ { get; set; }
}