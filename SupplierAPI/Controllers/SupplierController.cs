using Microsoft.AspNetCore.Mvc;
using SupplierAPI.Controllers.BaseControllers;
using SupplierAPI.Models.DTOs;
using SupplierAPI.Models.Enums;
using SupplierAPI.Services.Interfaces;

namespace SupplierAPI.Controllers;

[ApiController]
[Route("api/fornecedores")]
[Produces("application/json", "text/plain")]
public class SupplierController(ISupplierService supplierService)
   : BaseController<SupplierInputDto, SupplierOutputDto>
{

    /// <summary> Get all the suppliers </summary>
    /// <returns> A SupplierOutputDto list </returns>
    /// <response code="200"> All the suppliers on database </response>
    [HttpGet]
    [ProducesResponseType(typeof(List<SupplierOutputDto>), StatusCodes.Status200OK)]
    public override async Task<ActionResult<List<SupplierOutputDto>>> GetAll()
    {
        return Ok(await supplierService.GetAllSuppliers().ConfigureAwait(false));
    }

    /// <summary> Get the supplier with the given id </summary>
    /// <param name="id" > The supplier id to be retrived </param>
    /// <returns> A SupplierOutputDto with the supplier info of that id </returns>
    /// <response code="200"> If the given id correspond to an existing supplier id on database </response>
    /// <response code="404"> Error message, if the given id doesn't correspond to an existing supplier on database </response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SupplierOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<SupplierOutputDto>> GetById(int? id)
    {
        if (!TryGetIdValue(id, out int idValue)) return BadRequest("Provided id is invalid");

        var supplier = await supplierService.GetSupplierById(idValue).ConfigureAwait(false);
        if (supplier == null) return NotFound($"Supplier with id {id} not found");

        return Ok(supplier);
    }

    /// <summary> Create a new supplier with the info passed on the request body </summary>
    /// <param name="input"> A SupplierInputDto with all the needed information </param>
    /// <returns> An SupplierOutputDto with the newlly added Supplier info </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///       "Nome": "Name",
    ///       "Email": "email@example.com",
    ///       "Telefone": "5547992008870",
    ///       "RazaoSocial": "string example",
    ///       "CEP": 10000001,
    ///       "CNPJ": "01234567891011"
    ///     }
    ///
    /// </remarks>
    /// <response code="200"> If the supplier creation suceeded </response>
    /// <response code="400"> Error message, if the SupplierInputDto is invalid </response>
    [HttpPost]
    [ProducesResponseType(typeof(SupplierOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult<SupplierOutputDto>> Add([FromBody] SupplierInputDto input)
    {
        if (input == null) return BadRequest("Missing supplier information");

        return Ok(await supplierService.AddSupplier(input).ConfigureAwait(false));
    }

    /// <summary> Update the supplier with the given id with the info passed on the request body </summary>
    /// <param name="id"> The supplier id to be updated </param>
    /// <param name="input"> A SupplierInputDto with the information to be updated on the database </param>
    /// <returns> A SupplierOutputDto with all the current supplier information </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     {
    ///       "Nome": "Name",
    ///       "Email": "email@example.com",
    ///       "Telefone": "5547992008870",
    ///       "RazaoSocial": "string example",
    ///       "CEP": 10000001,
    ///       "CNPJ": "01234567891011"
    ///     }
    ///
    /// </remarks>
    /// <response code="200"> If the supplier update suceeded </response>
    /// <response code="400"> Error message, if the SupplierInputDto or given id are invalid </response>
    /// <response code="404"> Error message, if the given id doesn't correspond to an existing supplier on database </response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SupplierOutputDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public override async Task<ActionResult<SupplierOutputDto>> Update(int? id, [FromBody] SupplierInputDto input)
    {
        if (!TryGetIdValue(id, out int idValue)) return BadRequest("Provided id is invalid");
        if (input == null) return BadRequest("Missing supplier information");

        var updatedSupplier = await supplierService.UpdateSupplier(idValue, input).ConfigureAwait(false);
        if (updatedSupplier == null) return NotFound($"Supplier with id {id} not found");

        return Ok(updatedSupplier);
    }

    /// <summary> Delete a supplier with the given id </summary>
    /// <param name="id"> The supplier id to be deleted </param>
    /// <response code="200"> Success message, if the supplier was deleted succesfully </response>
    /// <response code="400"> Error message, if the correspond supplier is already deleted </response>
    /// <response code="404"> Error message, if the given id doesn't correspond to an existing supplier on database </response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> Delete(int? id)
    {
        if (!TryGetIdValue(id, out int idValue)) return BadRequest("Provided id is invalid");

        var operationResult = await supplierService.DeleteSupplier(idValue).ConfigureAwait(false);

        return HandleOperationResult(new OperationResultDto()
        {
            ReferenceId = idValue,
            OperationResult = operationResult,
            SuccessfullMessage = $"Supplier with id {id} was successfully deleted",
            ErrorMessage = $"Supplier with id {id} already deleted",
        });
    }

    /// <summary> Restore a supplier with the given id </summary>
    /// <param name="id"> The supplier id to be restored </param>
    /// <response code="200"> Success message, if the supplier was restored succesfully </response>
    /// <response code="400"> Error message, if the correspond supplier is already active </response>
    /// <response code="404"> Error message, if the given id doesn't correspond to an existing supplier on database </response>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Restore(int? id)
    {
        if (!TryGetIdValue(id, out int idValue)) return BadRequest("Provided id is invalid");

        var operationResult = await supplierService.RestoreSupplier(idValue).ConfigureAwait(false);

        return HandleOperationResult(new OperationResultDto()
        {
            ReferenceId = idValue,
            OperationResult = operationResult,
            SuccessfullMessage = $"Supplier with id {id} was successfully restored",
            ErrorMessage = $"Supplier with id {id} already active",
        });
    }

    /// <summary> Delete permanently all the supliers with EntityStatus equal to 'Deleted' </summary>
    /// <response code="200"> Success message, if the suppliers with EntityStatus 'Deleted' were deleted permanently </response>
    /// <response code="400"> Error message, if some error occurred while deleting suppliers </response>
    [HttpDelete]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> PurgeSuppliers()
    {
        var operationResult = await supplierService.PurgeDeletedSuppliers().ConfigureAwait(false);

        return HandleOperationResult(new OperationResultDto()
        {
            OperationResult = operationResult,
            SuccessfullMessage = $"All the Suppliers with EntityStatus 'Deleted' have been permanently deleted",
            ErrorMessage = $"An error occurred while deleting suppliers permanently",
        });
    }

    private ActionResult HandleOperationResult(OperationResultDto operationDto)
    {
        return operationDto.OperationResult switch
        {
            OperationResult.Successfull => Ok(operationDto.SuccessfullMessage),
            OperationResult.NotFound => NotFound($"Supplier with id {operationDto.ReferenceId} not found"),
            OperationResult.NotPossible => BadRequest(operationDto.ErrorMessage),
            OperationResult.Failed => BadRequest(operationDto.ErrorMessage),
            _ => throw new NotImplementedException("Not implemented response type")
        };
    }
}