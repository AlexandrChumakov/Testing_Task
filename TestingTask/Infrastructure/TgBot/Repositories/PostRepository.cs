using Application.TgBot.Interfaces;
using Dapper;
using Domain.TgBot.Models;
using Npgsql;

namespace Infrastructure.TgBot.Repositories;

public class PostRepository(NpgsqlConnection npgsqlConnection) : IPostRepository
{
    public async Task<List<TgPost>> TakePostsAsync()
    {
        const string sql = @"SELECT * FROM posts";
        var posts = await npgsqlConnection.QueryAsync<TgPost>(sql);
        return posts.ToList();
    }
    

    public async Task<List<TgPost>> TakeLastTenAsync()
    {
        const string sql = @"SELECT * FROM posts ORDER BY posts.date DESC LIMIT 10";
        var posts = await npgsqlConnection.QueryAsync<TgPost>(sql);
        return posts.ToList();
    }
}