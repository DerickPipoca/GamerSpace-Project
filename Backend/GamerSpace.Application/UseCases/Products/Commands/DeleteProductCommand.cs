using AutoMapper;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Commands
{
    public interface IDeleteProductCommand
    {
        Task Execute(long productId);
    }

    public class DeleteProductCommand : IDeleteProductCommand
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommand(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long productId)
        {
            var product = await _productRepository.GetByIdWithIncludesAsync(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            product.Disable();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}