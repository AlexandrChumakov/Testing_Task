using System.Data;
using System.Net;
using TestingTask.WebApi.Middleware.Exceptions;

namespace TestingTask.WebApi.Middleware;

public class HttpExceptionMiddleware(RequestDelegate next, ILogger<HttpExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (DuplicateNameException ex)
        {
            logger.LogTrace("User try to register with duplicate data {Exception}", ex.Message);
            await SendResponseHandler(context, (int)HttpStatusCode.BadRequest, ex.Message, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception: {Message}", ex.Message);
            await SendResponseHandler(context, (int)HttpStatusCode.InternalServerError, ex.Message, ex);
        }
    }

    private async Task SendResponseHandler(HttpContext context, int statusCode, string message, Exception exception)
    {
        if (context.Response.HasStarted)
            throw exception;

        var errorDetails = new ErrorDetailResponse(statusCode, message);
        var jsonResponse = errorDetails.ToString();

        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        context.Response.ContentLength = jsonResponse.Length;

        await context.Response.WriteAsync(jsonResponse);
    }
}