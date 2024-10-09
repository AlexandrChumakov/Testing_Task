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

    public async Task AddPostsAsync(List<Post> posts)
    {
        foreach (var post in posts)
        {
            const string sql = "call put_posts(@Title, @Date, @Description)";
            await ExecuteAsync(sql, new { post.Title, post.Date, post.Description });
        }
    }

    public async Task<List<Post>> TakePostsAsync()
    {
        using var dbConnection = Connection;
        const string sql = @"SELECT * FROM posts";
        var posts = await dbConnection.QueryAsync<Post>(sql);
        return posts.ToList();
    }

    public async Task<string> TakeTopTenAsync()
    {
        using var dbConnection = Connection;
        const string sql = @"SELECT * FROM get_top_ten()";
        var posts = await dbConnection.QueryAsync<string>(sql);
        var list = posts.ToList();
        return list[0];
    }

    public async Task<List<Post>> GetSortedAsync(DateTime from, DateTime to)
    {
        using var dbConnection = Connection;
        const string sql = @"SELECT * FROM get_sorted_posts(@From, @To)";
        var posts = await dbConnection.QueryAsync<Post>(sql, new { From = from, To = to });
        return posts.ToList();
    }

    private static async Task ExecuteAsync(string sql, object param)
    {
        using var dbConnection = Connection;
        await Connection.ExecuteAsync(sql, param);
    }
}