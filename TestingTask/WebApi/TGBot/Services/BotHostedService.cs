namespace TestingTask.WebApi.TGBot.Services;

public class BotHostedService(BotRunner botRunner) : BackgroundService
{
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await botRunner.StartAsync();
    }
}
