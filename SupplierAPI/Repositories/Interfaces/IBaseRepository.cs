using SupplierAPI.Models.Entities.BaseModels;
using SupplierAPI.Models.Enums;

namespace SupplierAPI.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> AddAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<OperationResult> DeleteAsync(int id);
    Task<OperationResult> RestoreEntity(int id);
    Task PurgeDeletedEntities();
}