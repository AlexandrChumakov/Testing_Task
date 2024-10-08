using Application.WebScraper.Interfaces;

namespace Application.WebScraper.Service;

public class PostInitializer(IPostParser postParser, IPostRepository postRepository)
{
    public async Task AddDefaultPosts() => await postRepository.AddPosts(await postParser.ParsePosts());

}