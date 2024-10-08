using System.Data;
using Application.Authentication.Interfaces;
using Dapper;
using Domain.Authentication.Models;
using Npgsql;

namespace Infrastructure.Authentication.Repositories;

public class UserRepository : IUserRepository
{
    private const string ConnString = "Host=localhost;Port=5431;Database=testedProj;Username=postgres;Password=password";

    private IDbConnection Connection => new NpgsqlConnection(ConnString);

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        using var dbConnection = Connection;
        var user = await Connection.QueryFirstOrDefaultAsync<User?>($"SELECT * FROM users WHERE phone = {phone}");
        return user;
    }

    public async Task CreateAsync(User user)
    {
        const string sql = "INSERT INTO users (phone, password) VALUES {@Phone, @Password}";
        object[] param = [new { Phone = user.Phone, Password = user.Password }];
        await ExecuteAsync(sql, param);
    }

    private async Task ExecuteAsync(string sql, object param)
    {
        using var dbConnection = Connection;
        await Connection.ExecuteAsync(sql, param);
    }
}
