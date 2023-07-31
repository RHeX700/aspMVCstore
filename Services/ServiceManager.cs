using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;

        private Lazy<CartItemService> _lazyCartItemService;
        private Lazy<CartService> _lazyCartService;
        private Lazy<OrderItemService> _lazyOrderItemService;
        private Lazy<OrderService> _lazyOrderService;
        private Lazy<CategoryService> _lazyCategoryService;
        private Lazy<ProductService> _lazyProductService;

        public ServiceManager(IRepositoryManager repositoryManager) { 
            _repositoryManager = repositoryManager;
            _lazyCartItemService = new Lazy<CartItemService>(() =>
            {
                CartItemService cartItemService = new CartItemService(_repositoryManager);
                return cartItemService;
            });

            _lazyCartService = new Lazy<CartService>(() =>
            {
                CartService cartService = new CartService(_repositoryManager);
                return cartService;
            });

            _lazyOrderItemService = new Lazy<OrderItemService>(() =>
            {
                OrderItemService orderItemService = new OrderItemService(_repositoryManager);
                return orderItemService;
            });

            _lazyOrderService = new Lazy<OrderService>(() =>
            {
                OrderService orderService = new OrderService(_repositoryManager);
                return orderService;
            });

            _lazyCategoryService = new Lazy<CategoryService>(() =>
            {
                CategoryService categoryService = new CategoryService(_repositoryManager);
                return categoryService;
            });

            _lazyProductService = new Lazy<ProductService>(() =>
            {
                ProductService productService = new ProductService(_repositoryManager);
                return productService;
            });
        }
        public ICartItemService CartItemService => _lazyCartItemService.Value;

        public ICartService CartService => _lazyCartService.Value;

        public ICategoryService CategoryService => _lazyCategoryService.Value;

        public IOrderItemService OrderItemService => _lazyOrderItemService.Value;

        public IOrderService OrderService => _lazyOrderService.Value;
        public IProductService ProductService => _lazyProductService.Value;

    }
}
