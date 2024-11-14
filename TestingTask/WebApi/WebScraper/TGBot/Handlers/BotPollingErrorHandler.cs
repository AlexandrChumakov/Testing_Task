using Telegram.Bot.Exceptions;

namespace TestingTask.WebApi.WebScraper.TGBot.Handlers;

public class BotPollingErrorHandler
{
    public Task HandlePollingErrorAsync(Exception exception, CancellationToken cancellationToken)
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