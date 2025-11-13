using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class CartProduct : EntityBase
    {
        public short Quantity { get; private set; }

        public long UserId { get; private set; }
        public User User { get; private set; } = null!;

        public long ProductVariantId { get; private set; }
        public ProductVariant ProductVariant { get; private set; } = null!;

        private CartProduct() { }

        public CartProduct(short quantity, long userId, long productVariantId)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity must be greater than 0.");
            if (userId <= 0)
                throw new ArgumentException("User id is invalid.");
            if (productVariantId <= 0)
                throw new ArgumentException("Product variant id is invalid.");
            Quantity = quantity;
            UserId = userId;
            ProductVariantId = productVariantId;
        }
    }
}