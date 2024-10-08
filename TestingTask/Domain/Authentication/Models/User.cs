namespace Domain.Authentication.Models;

public sealed class User
{
    public int Id { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
}