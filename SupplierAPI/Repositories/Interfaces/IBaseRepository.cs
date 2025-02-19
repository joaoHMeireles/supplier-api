using SupplierAPI.Models.BaseModels;

namespace SupplierAPI.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<int> AddAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    //  Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}