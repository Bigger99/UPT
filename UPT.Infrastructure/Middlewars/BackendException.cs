namespace UPT.Infrastructure.Middlewars;

public class BackendException : Exception
{
    public BackendException(string message)
        : base(message) { }
}
