using Microsoft.AspNetCore.Mvc;

namespace SupplierAPI.Controllers.BaseControllers;

public abstract class BaseController<Input, Output> : Controller
{
    public abstract Task<ActionResult<List<Output>>> GetAll();
    public abstract Task<ActionResult<Output>> GetById(int? id);
    public abstract Task<ActionResult<Output>> Add([FromBody] Input input);
    public abstract Task<ActionResult<Output>> Update(int? id, [FromBody] Input input);
    public abstract Task<ActionResult> Delete(int? id);

    protected bool TryGetIdValue(int? id, out int idValue)
    {
        if (id.HasValue && id.Value != 0)
        {
            idValue = id.Value;
            return true;
        }
        
        idValue = 0;
        return false;
    }
}