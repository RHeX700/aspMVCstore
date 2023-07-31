using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Orders;
using Contracts.Products;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllAsync(string OwnerID);
        Task<Order> GetByIdAsync(int OrderID);
        Task<Order> Insert(Order entity);
        Task<Order> Update(Order entity);
        void Delete(Order entity);
    }
}
