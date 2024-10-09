using System.ComponentModel.DataAnnotations;
using TestingTask.WebApi.Shared.Resources;

namespace TestingTask.WebApi.Authentication.DTOs;

public struct UserLoginDto
{
    [RegularExpression(@"^\d{10}$", ErrorMessageResourceType = typeof(Responses),
        ErrorMessageResourceName = "Format_phone")]
    [Required(ErrorMessageResourceType = typeof(Responses), ErrorMessageResourceName = "Required_phone")]
    public required string Phone { get; set; }

    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]?).{9,}$",
        ErrorMessageResourceType = typeof(Responses),
        ErrorMessageResourceName = "Format_pass")]
    [Required(ErrorMessageResourceType = typeof(Responses), ErrorMessageResourceName = "Required_pass")]
    public required string Password { get; set; }
}