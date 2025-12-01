using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class ProductVariant : EntityBase
    {
        public string SKU { get; private set; } = String.Empty;
        public decimal Price { get; private set; }
        public string? Description { get; private set; }
        public short StockAmount { get; private set; }
        public string? ImageUrl { get; private set; } = String.Empty;

        public long ProductId { get; private set; }
        public Product Product { get; private set; } = null!;

        public ICollection<OrderProduct> OrderProducts { get; private set; } = new List<OrderProduct>();

        private ProductVariant() { }

        public ProductVariant(string sku, decimal price, string? description, short stockAmount, string? imageUrl)
        {
            SKU = sku;
            Price = price;
            Description = description;
            StockAmount = stockAmount;
            ImageUrl = imageUrl;
        }

        public void Update(string? sku,decimal? price, string? description, short? stockAmount, string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new Exception();
            }
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new Exception();
            }
            if (price.HasValue && price.Value < 0)
                throw new Exception();

            if (stockAmount.HasValue && stockAmount.Value < 0)
                throw new Exception();

            SKU = sku;
            Price = price ?? Price;
            Description = description ?? Description;
            StockAmount = stockAmount ?? StockAmount;
            ImageUrl = imageUrl;
        }

        public void RemoveStock(short quantity)
        {
            if (quantity < 0)
                throw new Exception();
            StockAmount -= quantity;
        }
    }
}