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

    public Task<List<T>> GetAllAsync()
    {
        return DBQuery
            .Where(x => x.EntityStatus == EntityStatus.Active)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }

    public Task<T?> GetByIdAsync(int id)
    {
        return GetByIdAsync(id, false);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        DBContext.Entry(entity).State = EntityState.Modified;

        await SaveChanges().ConfigureAwait(false);

        return entity;
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var dbEntity = await GetByIdAsync(id, true).ConfigureAwait(false);
        if (dbEntity == null) return OperationResult.NotFound;
        if (dbEntity.EntityStatus == EntityStatus.Deleted) return OperationResult.NotPossible;

        dbEntity.EntityStatus = EntityStatus.Deleted;
        await UpdateAsync(dbEntity).ConfigureAwait(false);

        return OperationResult.Successfull;
    }

    public async Task<OperationResult> RestoreEntity(int id)
    {
        var entity = await GetByIdAsync(id, true).ConfigureAwait(false);
        if(entity == null ) return OperationResult.NotFound;
        if(entity.EntityStatus == EntityStatus.Active) return OperationResult.NotPossible;

        entity.EntityStatus = EntityStatus.Active;
        await UpdateAsync(entity).ConfigureAwait(false);

        return OperationResult.Successfull;
    } 

    public async Task PurgeDeletedEntities()
    {
        var deletedEntities = await DBQuery
            .Where(x => x.EntityStatus == EntityStatus.Deleted)
            .ToListAsync()
            .ConfigureAwait(false);

        SettedDB.RemoveRange(deletedEntities);
        await SaveChanges().ConfigureAwait(false);
    }

    protected Task<T?> GetByIdAsync(int id, bool getDeleted = false)
    {
        return DBQuery
            .Where(x => x.Id == id && (
                x.EntityStatus == EntityStatus.Active 
                || getDeleted
            ))
            .FirstOrDefaultAsync();
    }
}