namespace GamerSpace.Application.DTOs
{
    public class UpdateProductVariantDto
    {
        public string? SKU { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public short? StockAmount { get; set; }
    }
}