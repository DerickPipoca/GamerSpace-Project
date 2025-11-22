using GamerSpace.Domain.Entities;

namespace GamerSpace.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetByIdWithVariantsAsync(long productId);
        Task<List<ProductVariant>> GetVariantsByIdsAsync(List<long> productVariantId);
        Task<ProductVariant?> GetVariantByIdAsync(long productVariantId);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(long categoryId);
        Task<Product?> GetByIdWithCategoriesAsync(long productId);
        Task<(IEnumerable<Product>, long)> GetAllPaginatedAsync(int page, int pageSize, List<long>? categoryIds, String? searchTerm);
    }
}