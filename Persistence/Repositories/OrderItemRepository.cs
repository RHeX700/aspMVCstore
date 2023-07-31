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
    internal class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItem.ToListAsync();
        }

        public async Task<OrderItem> GetByIdAsync(int OrderID, int ProductID)
        {
            return await _context.OrderItem.FirstOrDefaultAsync(o => o.OrderID == OrderID && o.ProductID == ProductID);
        }

        public async Task<OrderItem> Insert(OrderItem entity)
        {
            var orderItem = _context.Add(entity);
            await _context.SaveChangesAsync();
            return orderItem.Entity;
        }

        public async void Remove(OrderItem entity)
        {
            var orderItem = _context.Remove(entity);
            await _context.SaveChangesAsync();
            return ;
        }

        public async Task<OrderItem> Update(OrderItem entity)
        {
            var cart = _context.Update(entity);
            await _context.SaveChangesAsync();
            return cart.Entity;
        }
    }
}
