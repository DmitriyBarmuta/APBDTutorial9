namespace Tutorial9.Exceptions;

public class ProcedureExecutionException : Exception
{
    public ProcedureExecutionException(string? message) : base(message)
    {
    }
}