namespace Application.Authentication.Interfaces;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string hash);
    
}