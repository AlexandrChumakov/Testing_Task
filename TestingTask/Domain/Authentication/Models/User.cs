namespace Domain.Authentication.Models;

public struct User
{
    public int Id { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
}