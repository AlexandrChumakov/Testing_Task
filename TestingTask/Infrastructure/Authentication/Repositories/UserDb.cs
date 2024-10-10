using Dapper;
using Npgsql;

namespace Infrastructure.Authentication.Repositories;

public class UserDb(NpgsqlConnection npgsqlConnection)
{
    public async Task CreateTablesAsync()
    {
        const string sql = @"create table if not exists users
(
    id       serial
        primary key,
    phone    varchar not null,
    password varchar not null
);

alter table users
    owner to postgres;

";
        await npgsqlConnection.ExecuteAsync(sql);
    }
}