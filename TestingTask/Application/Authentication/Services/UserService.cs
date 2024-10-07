using System.Data;
using Application.Authentication.Interfaces;
using Application.Resources;
using Domain.Authentication.Models;

namespace Application.Authentication.Services;

public sealed class UserService(IUserRepository repository, IPasswordHasher passwordHasher) : IUserService
{
    public async Task RegisterUserAsync(User user)
    {
        var identity = await repository.GetByPhoneAsync(user.Phone);

        if (identity is not null)
            throw new DuplicateNameException(Responses.User_alredy_exist);

        await repository.CreateAsync(user);
    }

    public async Task<User?> LoginAsync(User request)
    {
        var user = await repository.GetByPhoneAsync(request.Phone);
        if (user is null)
            return user;
        
        var isPasswordCorrect = passwordHasher.Verify(request.Password, user.Password);
        return !isPasswordCorrect ? null : user;
    }
}