namespace Tutorial9.Exceptions;

public class InvalidProductIdException : Exception
{
    public InvalidProductIdException(string? message) : base(message)
    {
    }
}