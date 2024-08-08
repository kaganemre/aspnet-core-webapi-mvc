namespace GuitarShopApp.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(): base("Not Found") 
    { 

    }
}