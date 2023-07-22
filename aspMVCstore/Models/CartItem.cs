namespace aspMVCstore.Models
{
    public class CartItem
    {
        public Guid CartID { get; set;}
        public int ProductID { get; set;}
        public int Quantity { get; set;}
        public Cart Cart { get; set;}
        public Product Product { get; set;}
        public decimal TotalPrice { get; set; }

    }
}
