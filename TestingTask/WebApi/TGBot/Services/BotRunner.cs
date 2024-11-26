using Application.TgBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TestingTask.WebApi.TGBot.Handlers;

namespace TestingTask.WebApi.TGBot.Services;

public sealed class BotRunner(
    TelegramBotClient botClient,
    CancellationTokenSource cancellationTokenSource,
    IBotUpdateHandler updateHandler,
    CallbackQueryHandler callbackQueryHandler)
{
    public async Task StartAsync()
    {
        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Bot run: @{me.Username}");

        botClient.StartReceiving(
            HandleUpdateAsync,
            (_, exception, _) =>
                BotPollingErrorHandler.HandlePollingErrorAsync(exception),
            receiverOptions: new Telegram.Bot.Polling.ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            },
            cancellationToken: cancellationTokenSource.Token);

        await Task.Delay(-1, cancellationTokenSource.Token);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient telegramBotClient, Update update,
        CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await updateHandler.HandelUpdateAsync(update, cancellationToken);
                    break;
                case UpdateType.CallbackQuery:
                    await callbackQueryHandler.HandelUpdateAsync(update, cancellationToken);
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing updates: {ex.Message}");
        }
    }

    public void Stop()
    {
        cancellationTokenSource.Cancel();
        Console.WriteLine("Bot stoped.");
    }
}