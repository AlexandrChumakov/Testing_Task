namespace TestingTask.WebApi.TGBot.Services;

public class BotHostedService(BotRunner botRunner) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await botRunner.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        botRunner.Stop();
        return Task.CompletedTask;
    }
}