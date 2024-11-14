using Application.TgBot.Interfaces;
using TestingTask.WebApi.TGBot.Handlers;

namespace TestingTask.WebApi.TGBot.Services;

public static class BotRegistration
{
    public static void AddTgBotServices(this IServiceCollection collection)
    {
        collection.AddTransient<IBotUpdateHandler, MessageHandler>();
        collection.AddTransient<BotPollingErrorHandler>();
        collection.AddTransient<CancellationTokenSource>();
        collection.AddTransient<BotRunner>(); 
    }  
}