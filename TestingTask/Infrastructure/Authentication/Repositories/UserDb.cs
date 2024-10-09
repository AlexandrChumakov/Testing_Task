using System.Data;
using Dapper;
using Npgsql;

namespace Infrastructure.Authentication.Repositories;

public class UserDb
{
    private const string ConnString =
        "Host=localhost;Port=5431;Database=testedProj;Username=postgres;Password=password";

    private static IDbConnection Connection => new NpgsqlConnection(ConnString);

    public async Task CreateTablesAsync()
    {
        var sql = @"create table if not exists users
(
    id       serial
        primary key,
    phone    varchar not null,
    password varchar not null
);

alter table users
    owner to postgres;

";
       await Connection.ExecuteAsync(sql);
    }
}