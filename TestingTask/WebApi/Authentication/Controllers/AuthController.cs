using Application.Authentication.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TestingTask.WebApi.Authentication.DTOs;
using TestingTask.WebApi.Authentication.Mapping;
using TestingTask.WebApi.Shared.Resources;

namespace TestingTask.WebApi.Authentication.Controllers;

[ApiController]
[Route("api/")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [Route("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType<string>(400)]
    [HttpPost]
    public async Task<ActionResult<UserRegisterDto>> LoginAsync([FromBody] UserLoginDto request)
    {
        var claimsPrincipal = await authService.LoginAsync(request.LoginDtoToUser());
        if (claimsPrincipal is null)
            return BadRequest(Responses.Invalid_user_data);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal);
        return Ok();
    }

    [Route("register")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [HttpPost]
    public async Task<ActionResult<UserRegisterDto>> RegisterAsync([FromBody] UserRegisterDto request)
    {
        await authService.RegisterAsync(request.RegisterDtoToUser());

        return Ok();
    }
}