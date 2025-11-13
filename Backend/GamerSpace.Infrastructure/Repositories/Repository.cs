using GamerSpace.Domain.Bases;
using GamerSpace.Domain.Interfaces;
using GamerSpace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GamerSpace.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly GamerSpaceDbContext _context;

        public Repository(GamerSpaceDbContext gamerSpaceDbContext)
        {
            _context = gamerSpaceDbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(long id) => await _context.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;

        public void Delete(T entity) => _context.Set<T>().Remove(entity);
    }
}