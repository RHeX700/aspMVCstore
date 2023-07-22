using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspMVCstore.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OwnerID { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal sum = 0;
                if (OrderItems != null)
                {
                    foreach (OrderItem item in OrderItems)
                    {
                        sum += item.TotalPrice;
                    }
                }
                return sum;
            }
        }
        public OrderStatus Status { get; set; }
        public string Address { get; set; }
    }

    public enum OrderStatus
    {
        Processing,
        PaymentConfirmed,
        Shipped,
        Completed,
        Cancelled
    }
}
