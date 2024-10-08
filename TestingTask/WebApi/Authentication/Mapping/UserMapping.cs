using Domain.Authentication.Models;
using TestingTask.WebApi.Authentication.DTOs;

namespace TestingTask.WebApi.Authentication.Mapping;

public static class UserMapping
{
    public static User RegisterDtoToUser(this UserRegisterDto dto) =>
        new()
        {
            Phone = dto.Phone,
            Password = dto.Password
        };

    public static User LoginDtoToUser(this UserLoginDto dto) =>
        new()
        {
            Phone = dto.Phone,
            Password = dto.Password
        };
}