using Domain.WebScraper.Models;

namespace Application.WebScraper.Interfaces;

public interface IPostRepository
{
    Task AddPostsAsync(List<WebPost> posts);
    Task<List<WebPost>> TakePostsAsync();
    Task<string> TakeTopTenAsync();
    Task<List<WebPost>> GetSortedAsync(DateTime from, DateTime to);
    Task<List<WebPost>> GetContainsAsync(string word);
    
}