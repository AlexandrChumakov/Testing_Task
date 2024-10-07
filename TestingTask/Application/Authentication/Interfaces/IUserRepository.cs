using Domain.Authentication.Models;

namespace Application.Authentication.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User?> GetByPhoneAsync(string phone);
}