using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamerSpace.Application.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<CreateProductVariantDto>? Variants { get; set; }
    }
}