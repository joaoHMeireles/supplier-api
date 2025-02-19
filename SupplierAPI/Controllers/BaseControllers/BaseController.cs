using Microsoft.AspNetCore.Mvc;

namespace SupplierAPI.Controllers.BaseControllers;

public abstract class BaseController<Input, Output> : Controller
{
    public abstract Task<ActionResult<List<Output>>> GetAll();
    public abstract Task<ActionResult<Output>> GetById(int? id);
    public abstract Task<ActionResult<int>> Add([FromBody] Input input);
    public abstract Task<ActionResult<Output>> Update(int? id, [FromBody] Input input);
    public abstract Task<ActionResult> Delete(int? id);

    protected void CheckId(int? id)
    {
        if(!id.HasValue || id.Value == 0) throw new HttpRequestException("Invalid id on path");
    }
}