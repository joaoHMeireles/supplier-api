using SupplierAPI.DTOs;

namespace SupplierAPI.Services.Interfaces;

public interface ISupplierService
{
    Task<List<SupplierOutputDto>> GetAllSuppliers();
    Task<SupplierOutputDto> GetSupplierById(int id);
    Task<int> AddSupplier(SupplierInputDto inputDto);
    Task<SupplierOutputDto> UpdateSupplier(SupplierInputDto supplierInput);
    Task DeleteSupplier(int id);
}