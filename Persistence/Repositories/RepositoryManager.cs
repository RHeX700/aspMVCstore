using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        private Lazy<CartItemRepository> _lazyCartItemRepository;
        private Lazy<CartRepository> _lazyCartRepository;
        private Lazy<CategoryRepository> _lazyCategoryRepository;
        private Lazy<OrderItemRepository> _lazyOrderItemRepository;
        private Lazy<ProductRepository> _lazyProductRepository;
        private Lazy<OrderRepository> _lazyOrderRepository;

        public RepositoryManager(ApplicationDbContext context) {
            _context = context;

            _lazyCartItemRepository = new Lazy<CartItemRepository>(() => new CartItemRepository(_context));
            _lazyCartRepository = new Lazy<CartRepository>(() => new CartRepository(_context));
            _lazyCategoryRepository = new Lazy<CategoryRepository>(() => new CategoryRepository(_context));
            _lazyOrderItemRepository = new Lazy<OrderItemRepository>(() => new OrderItemRepository(_context));
            _lazyOrderRepository = new Lazy<OrderRepository>(() => new OrderRepository(_context));
            _lazyProductRepository = new Lazy<ProductRepository>(() => new ProductRepository(_context));
        }

        public ICartItemRepository CartItemRepository => _lazyCartItemRepository.Value;

        public ICartRepository CartRepository => _lazyCartRepository.Value;

        public ICategoryRepository CategoryRepository => _lazyCategoryRepository.Value;

        public IOrderItemRepository OrderItemRepository => _lazyOrderItemRepository.Value;

        public IOrderRepository OrderRepository => _lazyOrderRepository.Value;

        public IProductRepository ProductRepository => _lazyProductRepository.Value;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
    }
}
