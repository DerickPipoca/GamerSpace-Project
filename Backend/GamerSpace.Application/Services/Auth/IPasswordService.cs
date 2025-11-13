namespace GamerSpace.Application.Services.Auth
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string passwordHash);
    }
}