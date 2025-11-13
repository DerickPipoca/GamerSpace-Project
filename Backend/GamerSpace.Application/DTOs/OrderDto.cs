using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerSpace.Application.DTOs
{
    public class OrderDto
    {
        public long Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal ShippingAmount { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public long UserId { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; } = new();
    }
}