using AutoMapper;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Commands
{
    public interface IDeleteProductVariantCommand
    {
        Task Execute(long productId, long variantId);
    }

    public class DeleteProductVariantCommand : IDeleteProductVariantCommand
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductVariantCommand(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long productId, long variantId)
        {
            var product = await _productRepository.GetByIdWithIncludesAsync(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            product.RemoveVariant(variantId);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}