using GamerSpace.Domain.Interfaces;
using GamerSpace.Infrastructure.Persistence;

namespace GamerSpace.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GamerSpaceDbContext _context;

        public UnitOfWork(GamerSpaceDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}