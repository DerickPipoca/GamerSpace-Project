using GamerSpace.Application.DTOs;
using GamerSpace.Application.UseCases.Products.Commands;
using GamerSpace.Application.UseCases.Products.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerSpace.API.Controllers
{
    /// <summary>
    /// Gerencia as operações de produtos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
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
            IDeleteProductVariantCommand deleteProductVariantCommand
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
        }

        /// <summary>
        /// Lista todos os produtos com paginação.
        /// </summary>
        /// <param name="paginationQueryDto">Parâmetros de paginação (ex: página atual e tamanho da página).</param>
        /// <returns>Uma lista paginada de produtos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationQueryDto paginationQueryDto)
        {
            var pagedProducts = await _getAllProductsQuery.Execute(paginationQueryDto);
            return Ok(pagedProducts);
        }

        /// <summary>
        /// Obtém os detalhes de um produto específico pelo seu Id.
        /// </summary>
        /// <param name="id">Id do produto.</param>
        /// <returns>Os detalhes do produto.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            var product = await _getProductByIdQuery.Execute(id);
            return Ok(product);
        }

        /// <summary>
        /// Obtém os detalhes de uma variante de produto específica.
        /// </summary>
        /// <param name="productVariantId">Id da variante do produto.</param>
        /// <returns>Os detalhes da variante.</returns>
        [HttpGet("variants/{productVariantId}")]
        public async Task<IActionResult> GetProductVariantById(long productVariantId)
        {
            var product = await _getProductVariantByIdQuery.Execute(productVariantId);
            return Ok(product);
        }

        /// <summary>
        /// Lista todas as variantes de um produto específico.
        /// </summary>
        /// <param name="productId">Id do produto pai.</param>
        /// <returns>Uma lista de variantes associadas ao produto.</returns>
        [HttpGet("{productId}/variants")]
        public async Task<IActionResult> GetProductVariants(long productId)
        {
            var variants = await _getVariantsByProductQuery.Execute(productId);
            return Ok(variants);
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="createProductDto">Dados para a criação do produto.</param>
        /// <returns>O produto recém-criado.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProduct([FromBody] CreateProductDto createProductDto)
        {
            var product = await _createProductCommand.Execute(createProductDto);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Adiciona uma nova variante a um produto existente.
        /// </summary>
        /// <param name="productId">Id do produto pai.</param>
        /// <param name="createProductVariantDto">Dados para a criação da variante.</param>
        /// <returns>A variante recém-criada.</returns>
        [HttpPost("{productId}/variants")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProduct(long productId, [FromBody] CreateProductVariantDto createProductVariantDto)
        {
            var newVariantDto = await _addProductVariantCommand.Execute(productId, createProductVariantDto);

            return CreatedAtAction(nameof(GetProductVariantById), new { productVariantId = newVariantDto!.Id }, newVariantDto);
        }

        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        /// <param name="productId">Id do produto a ser atualizado.</param>
        /// <param name="updateProductDto">Novos dados do produto.</param>
        /// <returns>N/A</returns>
        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProduct(long productId, [FromBody] UpdateProductDto updateProductDto)
        {
            await _updateProductCommand.Execute(productId, updateProductDto);
            return NoContent();
        }

        /// <summary>
        /// Atualiza os dados de uma variante de produto.
        /// </summary>
        /// <param name="productId">Id do produto pai.</param>
        /// <param name="productVariantId">Id da variante a ser atualizada.</param>
        /// <param name="updateProductVariantDto">Novos dados da variante.</param>
        /// <returns>N/A</returns>
        [HttpPut("{productId}/variants/{productVariantId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProductVariant(long productId, long productVariantId, [FromBody] UpdateProductVariantDto updateProductVariantDto)
        {
            await _updateProductVariantCommand.Execute(productId, productVariantId, updateProductVariantDto);
            return NoContent();
        }

        /// <summary>
        /// Remove um produto e suas variantes do sistema.
        /// </summary>
        /// <param name="productId">Id do produto a ser removido.</param>
        /// <returns>N/A</returns>
        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            await _deleteProductCommand.Execute(productId);
            return NoContent();
        }

        /// <summary>
        /// Remove uma variante de produto do sistema.
        /// </summary>
        /// <param name="productId">Id do produto pai.</param>
        /// <param name="productVariantId">Id da variante a ser removida.</param>
        /// <returns>N/A</returns>
        [HttpDelete("{productId}/variants/{productVariantId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductVariant(long productId, long productVariantId)
        {
            await _deleteProductVariantCommand.Execute(productId, productVariantId);
            return NoContent();
        }
    }
}