using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class OrderProduct : EntityBase
    {
        public short Quantity { get; private set; }
        public decimal PurchasePrice { get; private set; }

        public long OrderId { get; private set; }
        public Order Order { get; private set; } = null!;

        public long ProductVariantId { get; private set; }
        public ProductVariant ProductVariant { get; private set; } = null!;

        private OrderProduct() { }

        public OrderProduct(short quantity, decimal purchasePrice, long orderId, long productVariantId)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity is invalid.");
            if (productVariantId <= 0)
                throw new ArgumentException("Product variant id is invalid.");
            if (purchasePrice <= 0)
                throw new ArgumentException("Purchase price is invalid.");
            Quantity = quantity;
            PurchasePrice = purchasePrice;
            OrderId = orderId;
            ProductVariantId = productVariantId;
        }
    }
}

