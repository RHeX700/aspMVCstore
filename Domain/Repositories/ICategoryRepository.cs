using Contracts.Categories;
using Contracts.Products;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetByIdAsync(int CategoryID);
        Task<Category> Insert(Category entity);
        Task<Category> Update(Category entity);
        void Delete(Category entity);
    }
}
