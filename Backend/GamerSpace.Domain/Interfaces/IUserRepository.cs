using GamerSpace.Domain.Entities;

namespace GamerSpace.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}