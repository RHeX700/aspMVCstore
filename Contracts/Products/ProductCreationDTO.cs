using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Products
{
    public class ProductCreationDTO : ICreationDto
    {
        public int? CategoryID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Uri? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int inventory { get; set; }

    }
}
