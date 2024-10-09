using Application.WebScraper.Interfaces;

namespace Application.WebScraper.Service;

public class PostInitializer(IPostParser postParser, IPostRepository postRepository)
{
    public async Task AddDefaultPostsAsync()
    {
        var posts = await postRepository.TakePostsAsync();
        if (posts.Count >= 30)
           return;

        await postRepository.AddPostsAsync(await postParser.ParsePostsAsync());
    }
}