using System.Security.Claims;
using Application.Authentication.Interfaces;
using Domain.Authentication.Models;

namespace TestingTask.WebApi.Authentication.Services;

public class AuthService(IUserService userService) : IAuthService
{
    public async Task<ClaimsPrincipal?> LoginAsync(User requst)
    {
        var user = await userService.LoginAsync(requst);
        if (user is null)
            return null;

        var claims = new List<Claim> { new(ClaimTypes.Name, user.Value.Phone) };
        var claimIdentity = new ClaimsIdentity(claims, "Cookies");
        return new ClaimsPrincipal(claimIdentity);
    }

    public Task RegisterAsync(User user) => userService.RegisterUserAsync(user);
}