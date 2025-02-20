using System.Diagnostics.CodeAnalysis;

namespace SupplierAPI.Models.DTOs;

public class SupplierOutputDto
{
    [NotNull]
    public required int Id { get; set; }
    
    [NotNull]
    public DateTime CreatedAt { get; set; }

    [NotNull]
    public required string Nome { get; set; }
    
    [NotNull]
    public required string Email { get; set; }
    public string? Telefone { get; set; }
    public string? RazaoSocial { get; set; }
    
    [NotNull]
    public required long CEP { get; set; }
    
    [NotNull]
    public required string CNPJ { get; set; }
    public string? EntityStatus { get; set; }
}