using Application.Authentication.Interfaces;
using Application.Authentication.Services;
using Infrastructure.Authentication.Ports.Security;
using Infrastructure.Authentication.Repositories;


namespace TestingTask.WebApi.Authentication.Services;

public static class AuthenticationRegistration
{
  public static void AddAuthenticationServices(this IServiceCollection collection)
  {
      collection.AddTransient<IUserService, UserService>();
      collection.AddTransient<IPasswordHasher, PasswordHasher>();
      collection.AddTransient<IUserRepository, UserRepository>();
  }
}