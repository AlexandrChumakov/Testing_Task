using Application.TgBot.Interfaces;
using Application.WebScraper.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TestingTask.WebApi.TGBot.Handlers;

public class CallbackQueryHandler(TelegramBotClient botClient, IPostRepository postRepository) : IBotUpdateHandler
{
    public async Task HandelUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.CallbackQuery?.Message?.Chat.Id;
        var data = update.CallbackQuery?.Data;

        switch (data)
        {
            case "PostsAll":
            {
                var posts = await postRepository.TakePostsAsync();
                foreach (var post in posts)
                {
                    await SendMessageAsync(chatId, post.ToString(), cancellationToken);
                }

                break;
            }
            case "PostsTen":
            {
                var posts = await postRepository.TakeLastTenAsync();
                foreach (var post in posts)
                {
                    await SendMessageAsync(chatId, post.ToString(), cancellationToken);
                }

                break;
            }
        }
    }
    
    private async Task SendMessageAsync(long? chatId, string text, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(chatId: chatId!, text: text, cancellationToken: cancellationToken);
    }
}