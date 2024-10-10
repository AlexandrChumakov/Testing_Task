using Application.Authentication.Interfaces;
using Dapper;
using Domain.Authentication.Models;
using Npgsql;

namespace Infrastructure.Authentication.Repositories;

public class UserRepository(NpgsqlConnection npgsqlConnection) : IUserRepository
{
    public async Task<User?> GetByPhoneAsync(string phone)
    {
        var user = await npgsqlConnection.QueryFirstOrDefaultAsync<User?>(
            "SELECT * FROM users WHERE phone = @Phone::varchar", new { Phone = phone });
        return user;
    }

    public async Task CreateAsync(User user)
    {
        const string sql = "INSERT INTO users (phone, password) VALUES (@Phone, @Password)";
        await ExecuteAsync(sql, new { user.Phone, user.Password });
    }

    private async Task ExecuteAsync(string sql, object param) => await npgsqlConnection.ExecuteAsync(sql, param);
}