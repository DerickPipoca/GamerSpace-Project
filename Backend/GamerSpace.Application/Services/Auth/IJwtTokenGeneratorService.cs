using GamerSpace.Domain.Entities;

namespace GamerSpace.Application.Services.Auth
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateToken(User user);
    }
}