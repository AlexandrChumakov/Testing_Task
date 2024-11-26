using Application.WebScraper.Interfaces;
using Dapper;
using Domain.WebScraper.Models;
using Npgsql;

namespace Infrastructure.WebScraper.Repositories;

public class PostRepository(NpgsqlConnection npgsqlConnection) : IPostRepository
{
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
        const string sql = @"SELECT * FROM posts";
        var posts = await npgsqlConnection.QueryAsync<Post>(sql);
        return posts.ToList();
    }

    public async Task<List<Post>> TakeLastTenAsync()
    {
        const string sql = @"SELECT * FROM posts ORDER BY posts.date DESC LIMIT 10";
        var posts = await npgsqlConnection.QueryAsync<Post>(sql);
        return posts.ToList();
    }

    public async Task<string> TakeTopTenAsync()
    {
        const string sql = @"SELECT * FROM get_top_ten()";
        var posts = await npgsqlConnection.QueryAsync<string>(sql);
        var list = posts.ToList();
        return list[0];
    }

    public async Task<List<Post>> GetSortedAsync(DateTime from, DateTime to)
    {
        const string sql = @"SELECT * FROM get_sorted_posts(@From, @To)";
        var posts = await npgsqlConnection.QueryAsync<Post>(sql, new { From = from, To = to });
        return posts.ToList();
    }

    public async Task<List<Post>> GetContainsAsync(string word)
    {
        const string sql = @"SELECT * FROM contains_in_posts(@Value)";
        var posts = await npgsqlConnection.QueryAsync<Post>(sql, new { Value = word });
        return posts.ToList();
    }

    private async Task ExecuteAsync(string sql, object param) => await npgsqlConnection.ExecuteAsync(sql, param);
}