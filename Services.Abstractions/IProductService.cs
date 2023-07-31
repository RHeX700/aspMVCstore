using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Abstractions
{
    public interface IProductService : IGenericService<Product>
    {
        Task<IQueryable<Product>> GetAllAsync(string? searchString);
        Task<Product> CreateAsync(Product product);
        Task<Product> GetByIdAsync(int ProductID);
        Task<Product> UpdateAsync(Product product);
        void DeleteAsync(int ProductID);
    }
}
