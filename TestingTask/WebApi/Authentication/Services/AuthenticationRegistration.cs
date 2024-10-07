using Application.Authentication.Interfaces;
using Application.Authentication.Services;
using Infrastructure.Authentication.Ports.Security;
using Infrastructure.Authentication.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace TestingTask.WebApi.Authentication.Services;

public static class AuthenticationRegistration
{
    public static void AddAuthenticationServices(this IServiceCollection collection)
    {
        collection.AddTransient<IUserService, UserService>();
        collection.AddTransient<IPasswordHasher, PasswordHasher>();
        collection.AddTransient<IUserRepository, UserRepository>();
        collection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => options.LoginPath = "/api/login");
    }
}