using Application.WebScraper.Interfaces;
using Application.WebScraper.Service;
using Infrastructure.WebScraper.Repositories;
using Infrastructure.WebScraper.Services;

namespace TestingTask.WebApi.WebScraper.Services;

public static class WebScraperRegistration
{
    public static void AddWebScraperServices(this IServiceCollection collection)
    {
        collection.AddTransient<IPostParser, PostParser>();
        collection.AddTransient<IPostRepository, PostRepository>();
        collection.AddTransient<PostInitializer>();
        collection.AddTransient<PostDb>();
    }
}