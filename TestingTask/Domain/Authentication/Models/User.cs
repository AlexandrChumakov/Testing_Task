namespace Domain.Authentication.Models;

public sealed class User
{
    public Guid? Id { get; set; }
    public string Phone { get; set; } 
    public string Password { get; set; }
}