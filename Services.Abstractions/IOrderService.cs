using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<IEnumerable<Order>> GetAllAsync(string OwnerID);
        Task<Order> CreateAsync(Order order);
        Task<Order> GetByIdAsync(int OrderID);
        Task<Order> UpdateAsync(Order order);
        void DeleteAsync(int OrderID);
    }
}
