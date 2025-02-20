using SupplierAPI.Models.Entities.BaseModels;

namespace SupplierAPI.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> AddAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id, bool getDeleted = false);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}