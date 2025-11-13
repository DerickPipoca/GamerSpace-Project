using GamerSpace.Domain.Bases;

namespace GamerSpace.Domain.Entities
{
    public class Order : EntityBase
    {
        public decimal ShippingAmount { get; private set; }
        public decimal SubTotalAmount { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public string? Status { get; private set; }

        public long UserId { get; private set; }
        public User User { get; private set; } = null!;

        //public long PaymentId { get; set; }
        //public Payment Payment { get; set; } = null!;

        public ICollection<OrderProduct> OrderProducts { get; private set; } = new List<OrderProduct>();

        private Order() { }

        public Order(decimal shippingAmount, decimal subTotalAmount, decimal discountAmount, long userId, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status is required.");
            if (userId <= 0)
                throw new ArgumentException("User id is invalid.");
            if (ShippingAmount < 0)
                throw new ArgumentException("Shipping amount is invalid.");
            if (SubTotalAmount < 0)
                throw new ArgumentException("Sub Total amount is invalid.");
            if (DiscountAmount < 0)
                throw new ArgumentException("Discount amount is invalid.");
            ShippingAmount = shippingAmount;
            SubTotalAmount = subTotalAmount;
            DiscountAmount = discountAmount;
            UserId = userId;
            Status = status;
        }

        public void AddOrderItem(OrderProduct item)
        {
            OrderProducts.Add(item);
        }
    }
}