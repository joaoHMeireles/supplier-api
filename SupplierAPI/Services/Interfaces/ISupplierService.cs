using SupplierAPI.Models.DTOs;
using SupplierAPI.Models.Enums;

namespace SupplierAPI.Services.Interfaces;

public interface ISupplierService
{
    Task<List<SupplierOutputDto>> GetAllSuppliers();
    Task<SupplierOutputDto?> GetSupplierById(int id);
    Task<SupplierOutputDto?> AddSupplier(SupplierInputDto supplierInput);
    Task<SupplierOutputDto?> UpdateSupplier(int id, SupplierInputDto supplierInput);
    Task<OperationResult> DeleteSupplier(int id);
    Task<OperationResult> RestoreSupplier(int id);
    Task<OperationResult> PurgeDeletedSuppliers();
}