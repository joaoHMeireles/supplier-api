using System.ComponentModel.DataAnnotations;
using SupplierAPI.Models.Entities.BaseModels;

namespace SupplierAPI.Models.Entities;

public class Supplier : BaseEntity
{
    [MaxLength(100)]
    public required string Nome { get; set; }
    
    [EmailAddress]
    public required string Email { get; set; }

    [Phone]
    public string? Telefone { get; set; }

    [MaxLength(150)]
    public string? RazaoSocial { get; set; }
    
    public required long CEP { get; set; }

    [StringLength(14, MinimumLength = 14)]
    public required string CNPJ { get; set; }
}