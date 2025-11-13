using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Queries
{
    public interface IGetVariantsByProductQuery
    {
        Task<IEnumerable<ProductVariantDto>?> Execute(long categoryId);
    }
    public class GetVariantsByProductQuery : IGetVariantsByProductQuery
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetVariantsByProductQuery(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVariantDto>?> Execute(long productId)
        {
            var product = await _productRepository.GetByIdWithVariantsAsync(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");

            var variantsDtos = _mapper.Map<IEnumerable<ProductVariantDto>>(product.Variants);

            return variantsDtos;
        }
    }
}