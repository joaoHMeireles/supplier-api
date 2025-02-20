using Microsoft.EntityFrameworkCore;
using SupplierAPI.Data;
using SupplierAPI.Models.Entities;
using SupplierAPI.Repositories.BaseRepositories;
using SupplierAPI.Repositories.Interfaces;

namespace SupplierAPI.Repositories;

public class SupplierRepository(SupplierApiContext dbContext) : 
    BaseRepository<Supplier>(dbContext), ISupplierRepository
{
}