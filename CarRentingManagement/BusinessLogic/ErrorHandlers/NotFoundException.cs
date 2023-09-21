﻿using System.Net;

namespace BusinessLogic.ErrorHandlers;

public class NotFoundException: BaseException
{
    public NotFoundException()
    {
        StatusCode = (int)HttpStatusCode.NotFound;
        Title = "Resource not found.";
    }

    public NotFoundException(string? message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.NotFound;
        Title = "Resource not found.";
    }
}