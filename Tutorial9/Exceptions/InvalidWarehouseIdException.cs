namespace Tutorial9.Exceptions;

public class InvalidWarehouseIdException : Exception
{
    public InvalidWarehouseIdException(string? message) : base(message)
    {
    }
}