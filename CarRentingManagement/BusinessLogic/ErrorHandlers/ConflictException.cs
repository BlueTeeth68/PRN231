using System.Net;

namespace BusinessLogic.ErrorHandlers;

public class ConflictException : BaseException
{
    public ConflictException()
    {
        StatusCode = (int)HttpStatusCode.Conflict;
        Title = "Resource conflict.";
    }

    public ConflictException(string? message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.Conflict;
        Title = "Resource conflict.";
    }
}