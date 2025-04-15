using Application.TgBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TestingTask.WebApi.TGBot.Handlers;

public class MessageHandler(TelegramBotClient botClient) : IBotUpdateHandler
{
    public async Task HandelUpdateAsync(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.Message!.Chat.Id;
        var text = update.Message!.Text;
        var replyKeyBord = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Все новости", "PostsAll"),
                InlineKeyboardButton.WithCallbackData("Последнии 10 новостей", "PostsTen"),
            }
        });
        if (text is "Все новости" or "News")
        {
            await SendMessageAsync(chatId, "Выберите формат", replyKeyBord, cancellationToken);

            return;
        }

        await SendMessageAsync(chatId, text!, null, cancellationToken);
    }

    private async Task SendMessageAsync(long chatId, string text, IReplyMarkup? replyMarkup,
        CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(chatId: chatId, replyMarkup: replyMarkup, text: text,
            cancellationToken: cancellationToken, parseMode: ParseMode.Html);
    }
}