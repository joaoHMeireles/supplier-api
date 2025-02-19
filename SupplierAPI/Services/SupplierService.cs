using AutoMapper;
using SupplierAPI.DTOs;
using SupplierAPI.Exceptions;
using SupplierAPI.Models;
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
    
    public async Task<SupplierOutputDto> GetSupplierById(int id)
    {
        var supplier = await supplierRepository.GetByIdAsync(id).ConfigureAwait(false) 
            ?? throw new EntityNotFoundException();
        return FromEntityToOutput(supplier);
    }

    public async Task<int> AddSupplier(SupplierInputDto supplierInput)
    {
        var newSupplier = FromInputToEntity(supplierInput);
        return await supplierRepository.AddAsync(newSupplier).ConfigureAwait(false);
    }

    public async Task<SupplierOutputDto> UpdateSupplier(SupplierInputDto supplierInput)
    {
        // var supplier = FromInputToEntity(supplierInput);
        // var updatedSupplier = await supplierRepository.UpdateAsync(supplier).ConfigureAwait(false);
        // return FromEntityToOutput(updatedSupplier);
        return null;
    }
    
    public async Task DeleteSupplier(int id)
    {
        var successfullyDeleted = await supplierRepository.DeleteAsync(id).ConfigureAwait(false);

        if(!successfullyDeleted) throw new EntityNotFoundException();
    }

    private Supplier FromInputToEntity(SupplierInputDto supplierInput)
    {
        return mapper.Map<Supplier>(supplierInput);
    }

    private SupplierOutputDto FromEntityToOutput(Supplier supplier)
    {
        return mapper.Map<SupplierOutputDto>(supplier);;
    }
}