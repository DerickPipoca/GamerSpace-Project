namespace GamerSpace.Application.DTOs
{
    public class CheckoutDto
    {
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();

        // public long AddressId { get; set; }
        // public string DiscountCode { get; set; }
    }
}