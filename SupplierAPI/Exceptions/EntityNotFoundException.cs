namespace SupplierAPI.Exceptions;

public class EntityNotFoundException(string message = EntityNotFoundException.DEFAULT_ERROR_MESSAGE) 
    : Exception(message)
{
    private const string DEFAULT_ERROR_MESSAGE = "Entity not found on database";
}