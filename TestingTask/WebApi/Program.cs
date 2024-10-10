using Npgsql;
using TestingTask.WebApi.Authentication.Services;
using TestingTask.WebApi.WebScraper.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddWebScraperServices();
services.AddTransient<NpgsqlConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddHostedService<AddDefaultsPosts>();
services.AddHostedService<AddDefaultTable>();
services.AddAuthenticationServices();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();