using Contracts.Products;
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
    internal class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async void Delete(Product entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<IQueryable<Product>> GetAllAsync(string? searchString)
        {
            var products = from p in _context.Product
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString)
                || p.Description.Contains(searchString));
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int ProductID)
        {
            return await _context.Product.FindAsync(ProductID);
        }

        public async Task<Product> Insert(Product entity)
        {
            var product = _context.Add(entity);
            await _context.SaveChangesAsync();
            return product.Entity;
        }

        public async Task<Product> Update(Product entity)
        {
            var product = _context.Update(entity);
            await _context.SaveChangesAsync();
            return product.Entity;
        }
    }
}
