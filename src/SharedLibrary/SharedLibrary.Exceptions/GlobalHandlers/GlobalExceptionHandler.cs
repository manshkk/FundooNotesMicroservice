using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Exceptions.Exceptions;
using SharedLibrary.Exceptions.Responses;

namespace SharedLibrary.Exceptions.GlobalHandlers;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case ValidationException:
                statusCode = HttpStatusCode.BadRequest;
                break;

            case UnauthorizedOperationException:
                statusCode = HttpStatusCode.Unauthorized;
                break;

            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;

            case ConflictException:
                statusCode = HttpStatusCode.Conflict;
                break;
        }

        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(result);
    }
}