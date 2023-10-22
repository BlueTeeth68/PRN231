using BusinessLogic.ExceptionHandler;
using System.Net;

namespace Business_Logic.ExceptionHandler;

public class BadRequestException : BaseException
{
    private const int _statusCode = (int)HttpStatusCode.BadRequest;
    private const string? _title = "Bad Request.";

    public BadRequestException()
    {
        StatusCode = _statusCode;
        Title = _title;
    }

    public BadRequestException(string? message) : base(message)
    {
        StatusCode = _statusCode;
        Title = _title;
    }
}