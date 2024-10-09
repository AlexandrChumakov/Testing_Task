using Application.WebScraper.Interfaces;
using Domain.WebScraper.Models;

namespace Application.WebScraper.Service;

public sealed class PostService(IPostRepository postRepository) : IPostService
{

    public async Task<string> TakeTopTenAsync()
    {
        return await postRepository.TakeTopTenAsync();
    }

    public async Task<List<Post>> FilteredPostsAsync(DateTime from, DateTime to)
    {
        return await postRepository.GetSortedAsync(from, to);
    }
}