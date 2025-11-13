namespace GamerSpace.Application.DTOs
{
    public class ProductVariantDto
    {
        public long Id { get; set; }
        public string SKU { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public short StockAmount { get; set; }
    }
}