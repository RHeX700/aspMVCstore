using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        ICartItemService CartItemService { get; }
        ICartService CartService { get; }
        ICategoryService CategoryService { get; }
        IOrderItemService OrderItemService { get; }
        IOrderService OrderService { get; }
        IProductService ProductService { get; }
    }
}
