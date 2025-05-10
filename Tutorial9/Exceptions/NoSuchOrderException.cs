namespace Tutorial9.Exceptions;

public class NoSuchOrderException : Exception
{
    public NoSuchOrderException(string? message) : base(message)
    {
    }
}