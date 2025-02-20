using Microsoft.AspNetCore.Mvc;
using SupplierAPI.Controllers.BaseControllers;
using SupplierAPI.Models.DTOs;
using SupplierAPI.Services.Interfaces;

namespace SupplierAPI.Controllers;

[Route("api/fornecedores")]
[ApiController]
public class SupplierController(ISupplierService supplierService)
   : BaseController<SupplierInputDto, SupplierOutputDto>
{

    /// <summary> Get all the suppliers </summary>
    /// <returns> A SupplierOutputDto list </returns>
    [HttpGet]
    public override async Task<ActionResult<List<SupplierOutputDto>>> GetAll()
    {
        return Ok(await supplierService.GetAllSuppliers().ConfigureAwait(false));
    }

    /// <summary> Get the supplier with the given id </summary>
    /// <params> The supplier id to be retrived </params>
    /// <returns> A SupplierOutputDto with the supplier info of that id </returns>
    [HttpGet("{id}")]
    public override async Task<ActionResult<SupplierOutputDto>> GetById(int? id)
    {
        if (!TryGetIdValue(id, out int idValue)) return BadRequest("Provided id is invalid");
        
        var supplier = await supplierService.GetSupplierById(idValue).ConfigureAwait(false);
        if (supplier == null)  return NotFound($"Supplier with id {id} not found");
            
        return Ok(supplier);
    }

    /// <summary> Create a new supplier with the info passed on the Body </summary>
    /// <params> A SupplierInputDto with all the needed information</params>
    /// <returns> An SupplierOutputDto with the newlly added Supplier info </returns>
    [HttpPost]
    public override async Task<ActionResult<int>> Add([FromBody] SupplierInputDto input)
    {
        if (input == null) return BadRequest("Missing supplier information");
        
        var newSupplier = await supplierService.AddSupplier(input).ConfigureAwait(false);
        if (newSupplier == null)  return NotFound($"An error occured while creating the Supplier");

        return Ok(newSupplier);
    }

    /// <summary> Update a supplier with the info passed on the Body </summary>
    /// <params> A SupplierInputDto with the information to be updated on the database</params>
    /// <returns> A SupplierOutputDto with all the current supplier information </returns>
    [HttpPut("{id}")]
    public override async Task<ActionResult<SupplierOutputDto>> Update(int? id, [FromBody] SupplierInputDto input)
    {
        if (!TryGetIdValue(id, out int idValue)) return BadRequest("Provided id is invalid");
        if (input == null) return BadRequest("Missing supplier information");
        
        var updatedSupplier = await supplierService.UpdateSupplier(idValue, input).ConfigureAwait(false);
        if (updatedSupplier == null)  return NotFound($"Supplier with id {id} not found");

        return Ok(updatedSupplier);
    }

    /// <summary> Delete a supplier with the given id </summary>
    /// <params> The supplier id to be deleted </params>
    [HttpDelete("{id}")]
    public override async Task<ActionResult> Delete(int? id)
    {
        if (!TryGetIdValue(id, out int idValue)) return BadRequest("Provided id is invalid");
        
        var successfullOperation = await supplierService.DeleteSupplier(idValue).ConfigureAwait(false);
        if (!successfullOperation)  return NotFound($"Supplier with id {id} not found");

        return Ok($"Supplier with id {id} was successfully deleted");
    }
}