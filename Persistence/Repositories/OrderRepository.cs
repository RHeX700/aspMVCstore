using Contracts.Orders;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async void Delete(Order entity)
        {
            var order = _context.Remove(entity);
            await _context.SaveChangesAsync();
            return ;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync(string OwnerID)
        {
            return await _context.Order
                        .Where(o => o.OwnerID == OwnerID)
                        .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int OrderID)
        {
            return await _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderID == OrderID);
        }

        public async Task<Order> Insert(Order entity)
        {
            var order = _context.Add(entity);
            await _context.SaveChangesAsync();
            return order.Entity;
        }

        public async Task<Order> Update(Order entity)
        {
            var order = _context.Update(entity);
            await _context.SaveChangesAsync();
            return order.Entity;
        }
    }
}
