using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerSpace.Application.DTOs
{
    public class OrderProductDto
    {
        public long ProductVariantId { get; set; }
        public string SKU { get; set; } = string.Empty;
        public short Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}