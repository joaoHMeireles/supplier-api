using SupplierAPI.Models.Enums;

namespace SupplierAPI.Models.DTOs;

public class OperationResultDto
{
    public int? ReferenceId { get; set; }
    public OperationResult OperationResult { get; set; }
    public string? ErrorMessage { get; set; }
    public string? SuccessfullMessage { get; set; }
}