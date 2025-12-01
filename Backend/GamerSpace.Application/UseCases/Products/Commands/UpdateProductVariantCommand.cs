using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Commands
{
    public interface IUpdateProductVariantCommand
    {
        Task Execute(long productId, long productVariantId, UpdateProductVariantDto updateProductVariantDto);
    }
    public class UpdateProductVariantCommand : IUpdateProductVariantCommand
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductVariantCommand(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(long productId, long productVariantId, UpdateProductVariantDto updateProductVariantDto)
        {
            var product = await _productRepository.GetByIdWithVariantsAsync(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            product.UpdateVariant(
                productVariantId,
                updateProductVariantDto.SKU,
                updateProductVariantDto.Description,
                updateProductVariantDto.Price,
                updateProductVariantDto.StockAmount,
                updateProductVariantDto.ImageUrl
                );
                
            await _unitOfWork.SaveChangesAsync();
        }
    }
}