using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        public CartItemRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _context.CartItem.Include(c => c.Cart).Include(c => c.Product).ToListAsync();
        }

        public async Task<CartItem> GetByIdAsync(Guid CartID, int ProductID)
        {
            return await _context.CartItem
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartID == CartID && m.ProductID == ProductID);
        }

        public async Task<CartItem> Insert(CartItem entity)
        {
            var cartItem = _context.Add(entity);
            await _context.SaveChangesAsync();
            return cartItem.Entity; 
        }

        public async void Remove(CartItem entity)
        {
            var cartItem = _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItem> Update(CartItem entity)
        {
            var cartItem = _context.Update(entity);
            await _context.SaveChangesAsync();
            return cartItem.Entity;
        }

        public bool CartItemExists(Guid CartID, int ProductID)
        {
            return (_context.CartItem?.Any(e => e.CartID == CartID && e.ProductID == ProductID)).GetValueOrDefault();
        }
    }
}
