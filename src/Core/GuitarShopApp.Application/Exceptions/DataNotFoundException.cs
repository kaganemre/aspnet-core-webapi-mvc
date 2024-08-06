namespace GuitarShopApp.Application.Exceptions;

public class DataNotFoundException : Exception
{
    public DataNotFoundException(string type, object? id)
        : base($"The object with id value {id} of type {type} could not be found!") { }
}