using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;
using GamerSpace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GamerSpace.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly GamerSpaceDbContext _context;
        public CategoryRepository(GamerSpaceDbContext gamerSpaceDbContext) : base(gamerSpaceDbContext)
        {
            _context = gamerSpaceDbContext;
        }

        public async Task<IEnumerable<Category>> GetAllWithDetailsAsync()
        {
            return await CategoriesWithInclude().ToListAsync();
        }

        public async Task<Category?> GetByIdWithDetailsAsync(long categoryId)
        {
            return await CategoriesWithInclude().FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        private IQueryable<Category> CategoriesWithInclude()
        {
            return _context.Categories
                .AsNoTracking()
                .Include(c => c.Type);
        }
    }
}