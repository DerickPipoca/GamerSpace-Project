using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Commands
{
    public interface ICreateProductCommand
    {
        Task<ProductDto> Execute(CreateProductDto createProductDto);
    }
    public class CreateProductCommand : ICreateProductCommand
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommand(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Execute(CreateProductDto createProductDto)
        {
            var product = new Product(createProductDto.Name, createProductDto.Description);

            if (createProductDto.Variants != null)
            {
                foreach (var variantDto in createProductDto.Variants)
                {
                    var variant = new ProductVariant(
                        variantDto.SKU,
                        variantDto.Price,
                        variantDto.Description,
                        variantDto.StockAmount);

                    product.Variants.Add(variant);
                }
            }

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
    }
}