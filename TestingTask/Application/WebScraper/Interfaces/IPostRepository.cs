using Domain.WebScraper.Models;

namespace Application.WebScraper.Interfaces;

public interface IPostRepository
{
    Task AddPosts(List<Post> posts);
}