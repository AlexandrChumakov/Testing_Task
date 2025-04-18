using Application.WebScraper.Service;
using Infrastructure.Shared;

namespace TestingTask.WebApi.WebScraper.Services;

public class AddDefaultsPosts(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var scope = serviceProvider.CreateScope();
            var scope2 = serviceProvider.CreateScope();
            var postInitializer = scope2.ServiceProvider.GetRequiredService<PostInitializer>();
            var createDb = scope.ServiceProvider.GetRequiredService<PostDb>();

            await createDb.CreateTablesAsync().ConfigureAwait(false);
            await postInitializer.AddDefaultPostsAsync().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine("Add default posts data failed: {0}", e.Message);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}