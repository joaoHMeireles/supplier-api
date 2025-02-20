using System.ComponentModel;

namespace SupplierAPI.Models.Enums;

public enum EntityStatus
{
    [Description("Active")]
    Active,
    
    [Description("Deleted")]
    Deleted
}