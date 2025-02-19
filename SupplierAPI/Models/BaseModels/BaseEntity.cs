using System.ComponentModel.DataAnnotations;
using SupplierAPI.Enums;

namespace SupplierAPI.Models.BaseModels;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public EntityStatus EntityStatus { get; set; }
}