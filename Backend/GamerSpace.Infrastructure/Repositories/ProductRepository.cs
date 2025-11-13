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

        public async Task<(IEnumerable<Product>, long)> GetAllPaginatedAsync(int page, int pageSize)
        {
            var totalRecords = await _context.Products.CountAsync();

            var products = await _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalRecords);
        }

        public async Task<Product?> GetByIdWithCategoriesAsync(long productId)
        {
            var product = await _context.Products.Include(p => p.ProductCategories).FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<Product?> GetByIdWithVariantsAsync(long productId)
        {
            var product = await _context.Products.Include(p => p.Variants).FirstOrDefaultAsync(p => p.Id == productId);
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

        public async Task<List<ProductVariant>> GetVariantsByIdsAsync(List<long> productVariantsIds)
        {
            var productVariants = await _context.ProductVariants
                            .Where(pv => productVariantsIds.Contains(pv.Id) && !pv.Disabled).ToListAsync();
        
            return productVariants;
        }
    }
}