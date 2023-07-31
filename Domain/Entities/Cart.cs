using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Cart
    {
        public Guid CartID { get; set; }
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get {
                decimal sum = 0;
                if (CartItems != null)
                {
                    foreach (CartItem item in CartItems)
                    {
                        sum += item.TotalPrice;
                    }
                }
                return sum;
            } }
    }
}
