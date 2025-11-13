using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Queries
{
    public interface IGetAllProductsQuery
    {
        Task<PagedResultDto<ProductDto>> Execute(PaginationQueryDto paginationQueryDto);
    }

    public class GetAllProductsQuery : IGetAllProductsQuery
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQuery(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<ProductDto>> Execute(PaginationQueryDto paginationQueryDto)
        {
            var (products, totalRecords) = await _productRepository.GetAllPaginatedAsync(
                paginationQueryDto.PageNumber, paginationQueryDto.PageSize
            );

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

            return new PagedResultDto<ProductDto>(
                productDtos,
                totalRecords,
                paginationQueryDto.PageNumber,
                paginationQueryDto.PageSize
            );
        }
    }
}