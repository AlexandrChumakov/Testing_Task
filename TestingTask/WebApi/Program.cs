using Npgsql;
using Telegram.Bot;
using TestingTask.WebApi.Authentication.Services;
using TestingTask.WebApi.Middleware;
using TestingTask.WebApi.TGBot.Services;
using TestingTask.WebApi.WebScraper.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddWebScraperServices();
services.AddTransient<NpgsqlConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddHostedService<AddDefaultTable>();
services.AddHostedService<AddDefaultsPosts>();
services.AddHostedService<BotHostedService>();
services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
services.AddAuthenticationServices();
services.AddTgBotServices();
services.AddSingleton<TelegramBotClient>(_ => new TelegramBotClient(builder.Configuration["TelegramBotToken"]!));
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HttpExceptionMiddleware>();
app.MapControllers();
app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Run();