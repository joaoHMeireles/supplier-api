using Microsoft.EntityFrameworkCore;
using SupplierAPI.Data;
using SupplierAPI.Models.Entities.BaseModels;
using SupplierAPI.Models.Enums;
using SupplierAPI.Repositories.Interfaces;

namespace SupplierAPI.Repositories.BaseRepositories;

public abstract class BaseRepository<T>(SupplierApiContext dbContext) 
    : IBaseRepository<T> where T : BaseEntity
{
    protected SupplierApiContext DBContext { get; set; } = dbContext;
    protected DbSet<T> SettedDB => DBContext.Set<T>();
    protected IQueryable<T> DBQuery => SettedDB.AsNoTracking();

    protected async Task SaveChanges() => await DBContext.SaveChangesAsync();

    public async Task<T> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.EntityStatus = EntityStatus.Active;

        var dbEntity = await SettedDB.AddAsync(entity).ConfigureAwait(false);
        await SaveChanges().ConfigureAwait(false);

        return dbEntity.Entity;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await DBQuery
            .Where(x => x.EntityStatus == EntityStatus.Active)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<T?> GetByIdAsync(int id, bool getDeleted = false)
    {
        return await DBQuery
            .Where(x => x.Id == id && (
                x.EntityStatus == EntityStatus.Active 
                || getDeleted
            ))
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        DBContext.Entry(entity).State = EntityState.Modified;

        await SaveChanges().ConfigureAwait(false);

        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dbEntity = await GetByIdAsync(id).ConfigureAwait(false);

        if (dbEntity == null) return false;

        await DeleteEntity(dbEntity, false).ConfigureAwait(false);

        return true;
    }

    protected async Task DeleteEntity(T entity, bool fullDelete)
    {
        if (fullDelete)
        {
            SettedDB.Remove(entity);
            await SaveChanges().ConfigureAwait(false);
        }
        else
        {
            entity.EntityStatus = EntityStatus.Deleted;
            await UpdateAsync(entity).ConfigureAwait(false);
        }
    }
}