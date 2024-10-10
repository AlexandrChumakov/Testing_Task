using Application.Authentication.Interfaces;
using Domain.Authentication.Models;

namespace Application.Tests.MyMoq;

public class MockUserRepository : IUserRepository
{
    private readonly List<User> _users = [];

    public Task CreateAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByPhoneAsync(string phone)
    {
        var user = _users.FirstOrDefault(u => u.Phone == phone);
        return Task.FromResult(user);
    }
}