using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.Products.Commands;
using GamerSpace.Application.UseCases.Products.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerSpace.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGetAllProductsQuery _getAllProductsQuery;
        private readonly IGetProductByIdQuery _getProductByIdQuery;
        private readonly ICreateProductCommand _createProductCommand;
        private readonly IUpdateProductCommand _updateProductCommand;
        private readonly IDeleteProductCommand _deleteProductCommand;

        private readonly IGetVariantsByProductQuery _getVariantsByProductQuery;
        private readonly IGetProductVariantByIdQuery _getProductVariantByIdQuery;
        private readonly IAddProductVariantCommand _addProductVariantCommand;
        private readonly IUpdateProductVariantCommand _updateProductVariantCommand;
        private readonly IDeleteProductVariantCommand _deleteProductVariantCommand;

        private readonly IUpdateProductCategoriesCommand _updateProductCategoriesCommand;

        public ProductsController(
            IGetAllProductsQuery getAllProductsQuery,
            IGetProductByIdQuery getProductByIdQuery,
            IGetVariantsByProductQuery getVariantsByProductQuery,
            IUpdateProductCommand updateProductCommand,
            ICreateProductCommand createProductCommand,
            IGetProductVariantByIdQuery getProductVariantByIdQuery,
            IAddProductVariantCommand addProductVariantCommand,
            IUpdateProductVariantCommand updateProductVariantCommand,
            IDeleteProductCommand deleteProductCommand,
            IDeleteProductVariantCommand deleteProductVariantCommand,
            IUpdateProductCategoriesCommand updateProductCategoriesCommand
            )
        {
            _getAllProductsQuery = getAllProductsQuery;
            _getProductByIdQuery = getProductByIdQuery;
            _createProductCommand = createProductCommand;
            _updateProductCommand = updateProductCommand;
            _deleteProductCommand = deleteProductCommand;

            _getProductVariantByIdQuery = getProductVariantByIdQuery;
            _getVariantsByProductQuery = getVariantsByProductQuery;
            _addProductVariantCommand = addProductVariantCommand;
            _updateProductVariantCommand = updateProductVariantCommand;
            _deleteProductVariantCommand = deleteProductVariantCommand;

            _updateProductCategoriesCommand = updateProductCategoriesCommand;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationQueryDto paginationQueryDto)
        {
            var pagedProducts = await _getAllProductsQuery.Execute(paginationQueryDto);
            return Ok(pagedProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            var product = await _getProductByIdQuery.Execute(id);
            return Ok(product);
        }

        [HttpGet("variants/{productVariantId}")]
        public async Task<IActionResult> GetProductVariantById(long productVariantId)
        {
            var product = await _getProductVariantByIdQuery.Execute(productVariantId);
            return Ok(product);
        }

        [HttpGet("{productId}/variants")]
        public async Task<IActionResult> GetProductVariants(long productId)
        {
            var variants = await _getVariantsByProductQuery.Execute(productId);
            return Ok(variants);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProduct([FromBody] CreateProductDto createProductDto)
        {
            var product = await _createProductCommand.Execute(createProductDto);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPost("{productId}/variants")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProduct(long productId, [FromBody] CreateProductVariantDto createProductVariantDto)
        {
            var newVariantDto = await _addProductVariantCommand.Execute(productId, createProductVariantDto);

            return CreatedAtAction(nameof(GetProductVariantById), new { productVariantId = newVariantDto!.Id }, newVariantDto);
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProduct(long productId, [FromBody] UpdateProductDto updateProductDto)
        {
            await _updateProductCommand.Execute(productId, updateProductDto);
            return NoContent();
        }

        [HttpPut("{productId}/variants/{productVariantId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProductVariant(long productId, long productVariantId, [FromBody] UpdateProductVariantDto updateProductVariantDto)
        {
            await _updateProductVariantCommand.Execute(productId, productVariantId, updateProductVariantDto);
            return NoContent();
        }

        [HttpPut("{productId}/categories")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProductCategories(long productId, [FromBody] UpdateProductCategoriesDto updateProductCategoriesDto)
        {
            await _updateProductCategoriesCommand.Execute(productId, updateProductCategoriesDto.CategoryIds);
            return NoContent();
        }

        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            await _deleteProductCommand.Execute(productId);
            return NoContent();
        }

        [HttpDelete("{productId}/variants/{productVariantId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductVariant(long productId, long productVariantId)
        {
            await _deleteProductVariantCommand.Execute(productId, productVariantId);
            return NoContent();
        }
    }
}