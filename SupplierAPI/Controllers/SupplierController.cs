using Microsoft.AspNetCore.Mvc;
using SupplierAPI.Controllers.BaseControllers;
using SupplierAPI.DTOs;
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
        CheckId(id);

        return Ok(await supplierService.GetSupplierById(id.Value).ConfigureAwait(false));
    }

    /// <summary> Create a new supplier with the info passed on the Body </summary>
    /// <params> A SupplierInputDto with all the needed information</params>
    /// <returns> An id to the created supplier </returns>
    [HttpPost]
    public override async Task<ActionResult<int>> Add([FromBody] SupplierInputDto input)
    {
        var newSupplierId = await supplierService.AddSupplier(input).ConfigureAwait(false);
        return Ok(newSupplierId);
    }

    /// <summary> Update a supplier with the info passed on the Body </summary>
    /// <params> A SupplierInputDto with the information to be updated on the database</params>
    /// <returns> A SupplierOutputDto with all the current supplier information </returns>
    [HttpPut("{id}")]
    public override async Task<ActionResult<SupplierOutputDto>> Update(int? id, [FromBody] SupplierInputDto input)
    {
        CheckId(id);

        var updatedSupplier = await supplierService.UpdateSupplier(input).ConfigureAwait(false);

        return Ok(updatedSupplier);
    }


    /// <summary> Delete a supplier with the given id </summary>
    /// <params> The supplier id to be deleted </params>
    /// 
    [HttpDelete("{id}")]
    public override async Task<ActionResult> Delete(int? id)
    {
        CheckId(id);

        await supplierService.DeleteSupplier(id.Value).ConfigureAwait(false);

        return Ok();
    }
}