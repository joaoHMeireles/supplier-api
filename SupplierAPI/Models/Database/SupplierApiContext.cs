using Microsoft.EntityFrameworkCore;

namespace SupplierAPI.Models.Database;

public class SupplierApiContext : DbContext
{
    public DbSet<Supplier> Suppliers { get; set; }

    public string DbPath { get; }

    public SupplierApiContext(DbContextOptions<SupplierApiContext> options)
        : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "supplierapi.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}