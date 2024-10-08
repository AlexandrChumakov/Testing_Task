using Domain.WebScraper.Models;

namespace Application.WebScraper.Interfaces;

public interface IPostParser
{
    Task<List<Post>> ParsePosts();
}