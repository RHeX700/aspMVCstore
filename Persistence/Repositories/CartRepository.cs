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
    internal class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) {
            _context = context;
        }
        public async void Delete(Cart entity)
        {
            var cart = _context.Remove(entity);
            await _context.SaveChangesAsync();
            return ;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _context.Cart.ToListAsync();
        }

        public async Task<Cart> GetByIdAsync(Guid CartID)
        {
            return await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartID == CartID); 
        }

        public async Task<Cart> Insert(Cart entity)
        {
            var cart = _context.Add(entity);
            await _context.SaveChangesAsync();
            return cart.Entity;
        }

        public async Task<Cart> Update(Cart entity)
        {
            var cart = _context.Update(entity);
            await _context.SaveChangesAsync();
            return cart.Entity;
        }
    }
}
