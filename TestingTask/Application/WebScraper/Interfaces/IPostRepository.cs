using Domain.WebScraper.Models;

namespace Application.WebScraper.Interfaces;

public interface IPostRepository
{
    Task AddPostsAsync(List<Post> posts);
    Task<List<Post>> TakePostsAsync();
    Task<string> TakeTopTenAsync();
    Task<List<Post>> GetSortedAsync(DateTime from, DateTime to);
}