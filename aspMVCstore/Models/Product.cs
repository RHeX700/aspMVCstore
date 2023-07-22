using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspMVCstore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int? CategoryID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Uri? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int inventory { get; set; }
        public Category? category { get; set; }
    }
}
