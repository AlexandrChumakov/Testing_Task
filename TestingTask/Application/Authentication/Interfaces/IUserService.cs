using Domain.Authentication.Models;

namespace Application.Authentication.Interfaces;

public interface IUserService
{
    Task RegisterUserAsync(User user);
    Task<User?> LoginAsync(User request);
}