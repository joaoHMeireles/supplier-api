using Microsoft.EntityFrameworkCore;
using SupplierAPI.Models.Entities;

namespace SupplierAPI.Data;

public class SupplierApiContext(DbContextOptions<SupplierApiContext> options) : DbContext(options)
{
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Supplier>()
            .HasIndex(s => s.CNPJ)
            .IsUnique();
    }
}