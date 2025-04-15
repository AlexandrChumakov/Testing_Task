using Application.Authentication.Interfaces;
using Application.Authentication.Services;
using Application.Tests.MyMoq;
using Domain.Authentication.Models;
using Infrastructure.Authentication.Services;

namespace Application.Tests.Authentication;

public class UserServiceTests
{
    private readonly IUserService _userService;
    private readonly MockUserRepository _repository = new();

    public UserServiceTests()
    {
        _userService = new UserService(_repository, new PasswordHasher());
    }

    [Fact]
    public async Task RegisterUserAsync_ValidData_RegisterUserSuccessfully()
    {
        const string userPass = "12345678";
        var requestUser = new User
        {
            Phone = "7017762252",
            Password = userPass
        };
        await _userService.RegisterUserAsync(requestUser);

        var user = await _repository.GetByPhoneAsync(requestUser.Phone);

        Assert.IsType<User>(user);
        Assert.NotNull(user);
        Assert.NotNull(user.Password);
        Assert.NotNull(user.Phone);
        Assert.NotEqual(userPass, user.Password);
    }

    [Fact]
    public async Task RegisterUserAsync_UserAlreadyExists_ThrowsException()
    {
        var existingUser = new User
        {
            Phone = "7017762252",
            Password = "hashed_password"
        };
        await _repository.CreateAsync(existingUser);

        var requestUser = new User
        {
            Phone = "7017762252",
            Password = "123456"
        };

        var exception = await Assert.ThrowsAsync<System.Data.DuplicateNameException>(() => _userService.RegisterUserAsync(requestUser));
        Assert.True(exception.Message is "User already exists" or "Пользователь уже существует");
    }

    [Fact]
    public async Task LoginAsync_UserExistWithCorrectData_ReturnsUser()
    {
        var existingUser = new User
        {
            Phone = "7017762252",
            Password = "password"
        };
        await _userService.RegisterUserAsync(existingUser);

        var requestUser = new User
        {
            Phone = "7017762252",
            Password = "password"
        };
        var user = await _userService.LoginAsync(requestUser);
        Assert.IsType<User>(user);
        Assert.NotNull(user);
    }

    [Fact]
    public async Task LoginAsync_UserNotExist_ReturnsNull()
    {
        var requestUser = new User
        {
            Phone = "7017762252",
            Password = "password"
        };
        var user = await _userService.LoginAsync(requestUser);
        Assert.Null(user);
    }
    
    [Fact]
    public async Task LoginAsync_IncorrectData_ReturnsNull()
    {
        var existingUser = new User
        {
            Phone = "7017762252",
            Password = "password"
        };
        await _userService.RegisterUserAsync(existingUser);
        
        var requestUser = new User
        {
            Phone = "7017762252",
            Password = "wrongpass"
        };
        var user = await _userService.LoginAsync(requestUser);
        Assert.Null(user);
    }
}