namespace SupplierAPI.Models.DTOs;

public class SupplierOutputDto
{
    public required int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public string? Telefone { get; set; }
    public string? RazaoSocial { get; set; }
    public required long CEP { get; set; }
    public required string CNPJ { get; set; }
    public string? EntityStatus { get; set; }
}