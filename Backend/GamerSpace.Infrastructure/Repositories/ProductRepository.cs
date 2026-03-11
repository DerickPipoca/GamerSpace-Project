using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;
using GamerSpace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace GamerSpace.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly GamerSpaceDbContext _context;
        public ProductRepository(GamerSpaceDbContext gamerSpaceDbContext) : base(gamerSpaceDbContext)
        {
            _context = gamerSpaceDbContext;
        }

        public async Task<(IEnumerable<Product>, long)> GetAllPaginatedAsync(int page, int pageSize, List<long>? categoryIds, String? searchTerm)
        {
            var totalRecords = await _context.Products.CountAsync();

            var products = _context.Products.AsNoTracking().Include(p => p.Variants).Include(p => p.ProductCategories).AsQueryable();

            if (categoryIds != null && categoryIds.Any())
            {
                products = products.Where(p => p.ProductCategories.Any(pc => categoryIds.Contains(pc.CategoryId)));
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                products = products.Where(p =>
                    p.Name.Contains(searchTerm) ||
                    (p.Description != null && p.Description.Contains(searchTerm)));
            }

            var productsPaginated = await products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (productsPaginated, totalRecords);
        }

        public async Task<Product?> GetByIdWithIncludesAsync(long productId)
        {
            var product = await _context.Products.Include(p => p.Variants).Include(p => p.ProductCategories).FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(long categoryId)
        {
            var selectedProducts = _context.Products.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId));

            return await selectedProducts.ToListAsync();
        }

        public async Task<ProductVariant?> GetVariantByIdAsync(long productVariantId)
        {
            var productVariant = await _context.ProductVariants.FindAsync(productVariantId);
            return productVariant;
        }

        public async Task<List<ProductVariant>> GetVariantsByIdsAsync(List<long> productVariantIds)
        {
            var productVariants = await _context.ProductVariants
                            .Where(pv => productVariantIds.Contains(pv.Id) && !pv.Disabled).ToListAsync();

            return productVariants;
        }
    }
}