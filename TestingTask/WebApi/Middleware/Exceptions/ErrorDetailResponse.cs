using System.Text.Json;

namespace TestingTask.WebApi.Middleware.Exceptions;

public class ErrorDetailResponse(int statusCode, string message)
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}