using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Queries
{
    public interface IGetProductsByCategoryQuery
    {
        Task<IEnumerable<ProductDto>> Execute(long categoryId);
    }
    public class GetProductsByCategoryQuery : IGetProductsByCategoryQuery
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByCategoryQuery(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Execute(long categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);

            var productsDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productsDtos;
        }
    }
}