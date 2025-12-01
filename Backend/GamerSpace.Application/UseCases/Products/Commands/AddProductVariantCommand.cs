using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Commands
{
    public interface IAddProductVariantCommand
    {
        Task<ProductVariantDto> Execute(long productId, CreateProductVariantDto createProductVariantDto);
    }
    public class AddProductVariantCommand : IAddProductVariantCommand
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddProductVariantCommand(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductVariantDto> Execute(long productId, CreateProductVariantDto createProductVariantDto)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            var productVariant = new ProductVariant(
                createProductVariantDto.SKU,
                createProductVariantDto.Price,
                createProductVariantDto.Description,
                createProductVariantDto.StockAmount,
                createProductVariantDto.ImageUrl
                );

            product.AddVariant(productVariant);
            await _unitOfWork.SaveChangesAsync();
            
            return _mapper.Map<ProductVariantDto>(productVariant);
        }
    }
}