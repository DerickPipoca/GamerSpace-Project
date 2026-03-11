using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Queries
{
    public interface IGetProductByIdQuery
    {
        Task<ProductDto> Execute(long id);
    }

    public class GetProductByIdQuery : IGetProductByIdQuery
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQuery(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Execute(long id)
        {
            var product = await _productRepository.GetByIdWithIncludesAsync(id);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found.");

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }
    }
}