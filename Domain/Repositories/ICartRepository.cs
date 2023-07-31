using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Carts;
using Contracts.Products;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart> GetByIdAsync(Guid CartID);
        Task<Cart> Insert(Cart entity);
        Task<Cart> Update(Cart entity);
        void Delete(Cart entity);
    }
}
