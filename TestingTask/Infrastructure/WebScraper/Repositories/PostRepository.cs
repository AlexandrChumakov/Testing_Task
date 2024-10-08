using System.Data;
using Application.WebScraper.Interfaces;
using Dapper;
using Domain.WebScraper.Models;
using Npgsql;

namespace Infrastructure.WebScraper.Repositories;

public class PostRepository : IPostRepository
{
    private const string ConnString =
        "Host=localhost;Port=5431;Database=testedProj;Username=postgres;Password=password";

    private static IDbConnection Connection => new NpgsqlConnection(ConnString);

    public async Task AddPosts(List<Post> posts)
    {
        using var dbConnection = Connection;
        foreach (var post in posts)
        {
            const string sql = @"INSERT INTO posts (title, date, description) VALUES (@Title, @Date, @Description)";
            await ExecuteAsync(sql, new { post.Title, post.Date, post.Description });
        }
    }

    private static async Task ExecuteAsync(string sql, object param)
    {
        using var dbConnection = Connection;
        await Connection.ExecuteAsync(sql, param);
    }
}