using System.Security.Claims;
using Domain.Authentication.Models;

namespace Application.Authentication.Interfaces;

public interface IAuthService
{
    Task<ClaimsPrincipal?> LoginAsync(User user);
    Task RegisterAsync(User user);
}