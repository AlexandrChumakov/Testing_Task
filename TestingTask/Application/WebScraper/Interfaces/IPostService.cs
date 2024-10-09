using Domain.WebScraper.Models;

namespace Application.WebScraper.Interfaces;

public interface IPostService
{
    Task<string> TakeTopTenAsync();
    Task<List<Post>> FilteredPostsAsync(DateTime from, DateTime to);
}