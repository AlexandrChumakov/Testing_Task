using Telegram.Bot.Exceptions;

namespace TestingTask.WebApi.TGBot.Handlers;

public class BotPollingErrorHandler
{
    public static Task HandlePollingErrorAsync(Exception exception)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegramm Api Error \n [{apiRequestException.ErrorCode}] \n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}