using Infrastructure.Authentication.Repositories;

namespace TestingTask.WebApi.Authentication.Services;

public class AddDefaultTable(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var scope = serviceProvider.CreateScope();
            var uService = scope.ServiceProvider.GetRequiredService<UserDb>();

            await uService.CreateTablesAsync().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine("Add default tables failed: {0}", e.Message);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}