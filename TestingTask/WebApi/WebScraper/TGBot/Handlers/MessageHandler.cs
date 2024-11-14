using Application.TgBot.Interfaces;
using Application.WebScraper.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TestingTask.WebApi.WebScraper.TGBot.Handlers;

public class MessageHandler(TelegramBotClient botClient, IPostRepository postRepository) : IBotUpdateHandler
{
    
    public async Task HandelUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.Message!.Chat.Id;
        var text = update.Message!.Text;

        if (text is "Нововсти" or "News")
        {
            var posts = await postRepository.TakePostsAsync();
            foreach (var post in posts)
            {
                await SendMessageAsync(chatId, post.ToString(), cancellationToken);
            }

            return;
        }
        await SendMessageAsync(chatId, text!, cancellationToken);
    }

    private async Task SendMessageAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(chatId: chatId, text: text,
            cancellationToken: cancellationToken, parseMode: ParseMode.Html);
    }
}