using Telegram.Bot.Types;

namespace Application.TgBot.Interfaces;

public interface IBotUpdateHandler
{
   Task HandelUpdateAsync(Update update, CancellationToken cancellationToken);
}