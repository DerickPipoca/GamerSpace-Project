using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;
using GamerSpace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GamerSpace.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly GamerSpaceDbContext _context;
        public UserRepository(GamerSpaceDbContext gamerSpaceDbContext) : base(gamerSpaceDbContext)
        {
            _context = gamerSpaceDbContext;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper() && !u.Disabled);
            return user;
        }
    }
}