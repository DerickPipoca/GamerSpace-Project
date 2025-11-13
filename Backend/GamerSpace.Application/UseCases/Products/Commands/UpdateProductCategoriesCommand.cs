using AutoMapper;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Commands
{
    public interface IUpdateProductCategoriesCommand
    {
        Task Execute(long productId, List<long> newCategoryIds);
    }
    public class UpdateProductCategoriesCommand : IUpdateProductCategoriesCommand
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCategoriesCommand(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long productId, List<long> newCategoryIds)
        {
            var product = await _productRepository.GetByIdWithCategoriesAsync(productId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            var newCategoryIdsSet = new HashSet<long>(newCategoryIds);

            var categoriesToRemove = product.ProductCategories
                .Where(pc => !newCategoryIdsSet
                .Contains(pc.CategoryId)).ToList();

            var currentCategoryIds = new HashSet<long>(product.ProductCategories.Select(pc => pc.CategoryId));

            var categoryIdsToAdd = newCategoryIdsSet.Except(currentCategoryIds);

            foreach (var categoryToRemove in categoriesToRemove)
            {
                product.ProductCategories.Remove(categoryToRemove);
            }

            foreach (var categoryIdToAdd in categoryIdsToAdd)
            {
                product.ProductCategories.Add(new ProductCategory
                {
                    ProductId = productId,
                    CategoryId = categoryIdToAdd
                });
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}