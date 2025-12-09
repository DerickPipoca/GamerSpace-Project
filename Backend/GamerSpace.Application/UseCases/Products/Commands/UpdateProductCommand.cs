using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Entities;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Commands
{
    public interface IUpdateProductCommand
    {
        Task<ProductDto?> Execute(long productId, UpdateProductDto updateProductDto);
    }
    public class UpdateProductCommand : IUpdateProductCommand
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommand(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto?> Execute(long productId, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            product.Update(updateProductDto.Name, updateProductDto.Description, updateProductDto.CategoryIds);
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }
    }
}