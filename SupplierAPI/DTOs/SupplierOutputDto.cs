namespace SupplierAPI.DTOs;

public class SupplierOutputDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string RazaoSocial { get; set; }
    public int CEP { get; set; }
    public string CNPJ { get; set; }
}