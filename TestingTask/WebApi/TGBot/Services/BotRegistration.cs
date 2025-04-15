using Application.TgBot.Interfaces;
using Infrastructure.TgBot.Repositories;
using TestingTask.WebApi.TGBot.Handlers;

namespace TestingTask.WebApi.TGBot.Services;

public static class BotRegistration
{
    public static void AddTgBotServices(this IServiceCollection collection)
    {
        collection.AddTransient<IBotUpdateHandler, MessageHandler>();
        collection.AddTransient<IPostRepository, PostRepository>();
        collection.AddTransient<CallbackQueryHandler>();
        collection.AddTransient<BotPollingErrorHandler>();
        collection.AddTransient<CancellationTokenSource>();
        collection.AddTransient<BotRunner>(); 
    }  
}