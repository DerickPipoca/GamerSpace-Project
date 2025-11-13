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
        private readonly IMapper _mapper;
        public DeleteProductVariantCommand(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(long productId, long variantId)
        {
            var product = await _productRepository.GetByIdWithVariantsAsync(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            product.RemoveVariant(variantId);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}