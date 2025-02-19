using Microsoft.EntityFrameworkCore;
using SupplierAPI.Models;
using SupplierAPI.Models.Database;
using SupplierAPI.Repositories.BaseRepositories;
using SupplierAPI.Repositories.Interfaces;

namespace SupplierAPI.Repositories;

public class SupplierRepository(SupplierApiContext dbContext) : 
    BaseRepository<Supplier>(dbContext), ISupplierRepository
{
    public Task<Supplier?> GetByCNPJAsyncAsNoTracking(string cnpj)
    {
        return SettedDB.Where(x => x.CNPJ.Equals(cnpj))
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}