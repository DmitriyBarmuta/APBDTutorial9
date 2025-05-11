namespace Tutorial9.Exceptions;

public class DatabaseConnectionException : Exception
{
    public DatabaseConnectionException(string? message) : base(message)
    {
    }
}