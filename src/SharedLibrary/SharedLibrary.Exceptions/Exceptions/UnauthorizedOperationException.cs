namespace SharedLibrary.Exceptions.Exceptions;

public class UnauthorizedOperationException : Exception
{
    public UnauthorizedOperationException(string message)
        : base(message)
    {
    }
}