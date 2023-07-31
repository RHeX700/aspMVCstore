using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Products;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IQueryable<Product>> GetAllAsync(string? searchString);
        Task<Product> Insert(Product entity);
        Task<Product> Update(Product entity);
        void Delete(Product entity);
        Task<Product> GetByIdAsync(int ProductID);
    }
}
