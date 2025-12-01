namespace GamerSpace.Application.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}