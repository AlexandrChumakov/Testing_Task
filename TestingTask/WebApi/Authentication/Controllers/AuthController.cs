using System.Security.Claims;
using Application.Authentication.Interfaces;
using Domain.Authentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace TestingTask.WebApi.Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService) : ControllerBase
{
    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] User requst)
    {
        var user = await userService.LoginAsync(new User
        {
            Phone = requst.Phone,
            Password = requst.Password
        });
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Phone) };
        var claimIdentity = new ClaimsIdentity(claims, "Cookies");

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimIdentity));
        return Ok();
    }
}