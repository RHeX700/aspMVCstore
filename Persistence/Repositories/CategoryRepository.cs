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
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void Delete(Category entity)
        {
            var category = _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int CategoryID)
        {
            return await _context.Category.FindAsync(CategoryID);
        }

        public async Task<Category> Insert(Category entity)
        {
            var category = _context.Add(entity);
            await _context.SaveChangesAsync();
            return category.Entity;
        }

        public async Task<Category> Update(Category entity)
        {
            var category = _context.Update(entity);
            await _context.SaveChangesAsync();
            return category.Entity;
        }
    }
}
