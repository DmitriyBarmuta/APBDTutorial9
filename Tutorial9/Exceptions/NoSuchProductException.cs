namespace Tutorial9.Exceptions;

public class NoSuchProductException : Exception
{
    public NoSuchProductException(string? message) : base(message)
    {
    }
}