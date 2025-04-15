using Domain.TgBot.Models;

namespace Application.TgBot.Interfaces;

public interface IPostRepository
{
    Task<List<TgPost>> TakePostsAsync();
    Task<List<TgPost>> TakeLastTenAsync();
}