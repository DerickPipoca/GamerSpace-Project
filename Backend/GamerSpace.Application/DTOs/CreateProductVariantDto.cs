namespace GamerSpace.Application.DTOs
{
    public class CreateProductVariantDto
    {
        public string SKU { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public short StockAmount { get; set; }
    }
}