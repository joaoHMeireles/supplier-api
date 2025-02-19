using System.ComponentModel.DataAnnotations;
using SupplierAPI.Models.BaseModels;

namespace SupplierAPI.Models;

public class Supplier : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string Telefone { get; set; }

    [MaxLength(150)]
    public string RazaoSocial { get; set; }
    
    [Required]
    [StringLength(8, MinimumLength = 8)]
    public int CEP { get; set; }

    [Required]
    [StringLength(14, MinimumLength = 14)]
    public string CNPJ { get; set; }
}