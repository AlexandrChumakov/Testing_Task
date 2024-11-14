using Application.TgBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TestingTask.WebApi.WebScraper.TGBot.Handlers;

namespace TestingTask.WebApi.WebScraper.TGBot;

public sealed class BotRunner
{
    private readonly TelegramBotClient _botClient;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly BotPollingErrorHandler _pollingErrorHandler;
    private readonly IBotUpdateHandler _updateHandler;

    public BotRunner(TelegramBotClient botClient, CancellationTokenSource cancellationTokenSource,
        BotPollingErrorHandler pollingErrorHandler, IBotUpdateHandler updateHandler)
    {
        _botClient = botClient;
        _cancellationTokenSource = cancellationTokenSource;
        _pollingErrorHandler = pollingErrorHandler;
        _updateHandler = updateHandler;
    }

    public async Task StartAsync()
    {
        var me = await _botClient.GetMeAsync();
        Console.WriteLine($"Бот запущен: @{me.Username}");

        _botClient.StartReceiving(
            HandleUpdateAsync,
            (botClient, exception, cancellationToken) => 
                _pollingErrorHandler.HandlePollingErrorAsync(exception, cancellationToken),
            receiverOptions: new Telegram.Bot.Polling.ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            },
            cancellationToken: _cancellationTokenSource.Token);
        
        await Task.Delay(-1, _cancellationTokenSource.Token);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            await _updateHandler.HandelUpdateAsync(update, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка обработки обновления: {ex.Message}");
        }
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
        Console.WriteLine("Бот остановлен.");
    }
}
