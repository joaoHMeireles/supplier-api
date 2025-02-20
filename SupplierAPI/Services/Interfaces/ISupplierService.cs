using SupplierAPI.Models.DTOs;

namespace SupplierAPI.Services.Interfaces;

public interface ISupplierService
{
    Task<List<SupplierOutputDto>> GetAllSuppliers();
    Task<SupplierOutputDto?> GetSupplierById(int id);
    Task<SupplierOutputDto?> AddSupplier(SupplierInputDto supplierInput);
    Task<SupplierOutputDto?> UpdateSupplier(int id, SupplierInputDto supplierInput);
    Task<bool> DeleteSupplier(int id);
}