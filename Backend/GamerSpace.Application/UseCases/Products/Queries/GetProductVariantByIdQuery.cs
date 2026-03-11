using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GamerSpace.Application.DTOs;
using GamerSpace.Domain.Interfaces;

namespace GamerSpace.Application.UseCases.Products.Queries
{
    public interface IGetProductVariantByIdQuery
    {
        Task<ProductVariantDto> Execute(long id);
    }

    public class GetProductVariantByIdQuery : IGetProductVariantByIdQuery
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductVariantByIdQuery(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductVariantDto> Execute(long id)
        {
            var variant = await _productRepository.GetVariantByIdAsync(id);

            if (variant == null)
                throw new KeyNotFoundException($"Product variant with ID {id} not found.");

            return _mapper.Map<ProductVariantDto>(variant);
        }
    }
}