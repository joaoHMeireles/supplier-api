namespace SupplierAPI.Repositories.Interfaces;

public interface ISupplierRepository : IBaseRepository<Models.Supplier> 
{
    Task<Models.Supplier?> GetByCNPJAsyncAsNoTracking(string cnpj);
}