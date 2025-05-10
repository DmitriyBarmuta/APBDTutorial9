namespace Tutorial9.Exceptions;

public class AlreadyCompletedOrderException : Exception
{
    public AlreadyCompletedOrderException(string? message) : base(message)
    {
    }
}