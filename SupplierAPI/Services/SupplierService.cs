using AutoMapper;
using SupplierAPI.Models.DTOs;
using SupplierAPI.Models.Entities;
using SupplierAPI.Repositories.Interfaces;
using SupplierAPI.Services.Interfaces;

namespace SupplierAPI.Services;

public class SupplierService(
    ISupplierRepository supplierRepository,
    IMapper mapper
)
    : ISupplierService
{
    public async Task<List<SupplierOutputDto>> GetAllSuppliers()
    {
        var suppliers = await supplierRepository.GetAllAsync().ConfigureAwait(false);
        return mapper.Map<List<SupplierOutputDto>>(suppliers);
    }

    public async Task<SupplierOutputDto?> GetSupplierById(int id)
    {
        var supplier = await supplierRepository.GetByIdAsync(id, true).ConfigureAwait(false);
        return FromEntityToOutput(supplier);
    }

    public async Task<SupplierOutputDto?> AddSupplier(SupplierInputDto supplierInput)
    {
        var newSupplier = FromInputToEntity(supplierInput);
        var supplier = await supplierRepository.AddAsync(newSupplier).ConfigureAwait(false);
        return FromEntityToOutput(supplier);
    }

    public async Task<SupplierOutputDto?> UpdateSupplier(int id, SupplierInputDto supplierInput)
    {
        var dbSupplier = await supplierRepository.GetByIdAsync(id).ConfigureAwait(false);
        if(dbSupplier == null) return null;

        var supplier = FromInputToEntity(dbSupplier, supplierInput);
        supplier.Id = id;

        var updatedSupplier = await supplierRepository.UpdateAsync(supplier).ConfigureAwait(false);
        return FromEntityToOutput(updatedSupplier);
    }

    public async Task<bool> DeleteSupplier(int id)
    {
        return await supplierRepository.DeleteAsync(id).ConfigureAwait(false);
    }

    private Supplier FromInputToEntity(SupplierInputDto supplierInput)
    {
        return mapper.Map<Supplier>(supplierInput);
    }

    private Supplier FromInputToEntity(Supplier dbSupplier, SupplierInputDto supplierInput)
    {
        return mapper.Map(supplierInput, dbSupplier);
    }

    private SupplierOutputDto? FromEntityToOutput(Supplier? supplier)
    {
        return supplier == null ? null : mapper.Map<SupplierOutputDto>(supplier);
    }
}