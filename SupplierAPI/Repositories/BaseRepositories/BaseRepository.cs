using Microsoft.EntityFrameworkCore;
using SupplierAPI.Enums;
using SupplierAPI.Models.BaseModels;
using SupplierAPI.Repositories.Interfaces;

namespace SupplierAPI.Repositories.BaseRepositories;

public abstract class BaseRepository<T>(DbContext dbContext) : IBaseRepository<T> where T : BaseEntity
{
    protected DbContext DBContext { get; set; } = dbContext;
    protected DbSet<T> SettedDB => DBContext.Set<T>();
    protected IQueryable<T> DBQuery => SettedDB.AsNoTracking().Where(x => x.EntityStatus == EntityStatus.Active);

    protected async Task SaveChanges() => await DBContext.SaveChangesAsync();

    public async Task<int> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.EntityStatus = EntityStatus.Active;

        var dbEntity = await SettedDB.AddAsync(entity).ConfigureAwait(false);
        await SaveChanges().ConfigureAwait(false);

        return dbEntity.Entity.Id;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await DBQuery
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await DBQuery
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
    }

    // public async Task<T> UpdateAsync(T entity)
    // {
    // entity.UpdatedAt = DateTime.UtcNow;
    // // Update
    // Console.WriteLine("Updating the blog and adding a post");
    // blog.Url = "https://devblogs.microsoft.com/dotnet";
    // blog.Posts.Add(
    //     new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
    // await db.SaveChangesAsync();
    // }

    public async Task<bool> DeleteAsync(int id)
    {
        var dbEntity = await GetByIdAsync(id).ConfigureAwait(false);

        if (dbEntity == null) return false;

        DeleteEntity(dbEntity, false);

        await SaveChanges().ConfigureAwait(false);

        return true;
    }

    protected void DeleteEntity(T entity, bool fullDelete)
    {
        if (fullDelete)
        {
            SettedDB.Remove(entity);
        }
        else
        {
            entity.EntityStatus = EntityStatus.Deleted;
        }
    }
}