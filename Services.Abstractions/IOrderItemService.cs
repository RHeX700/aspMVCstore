
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderItemService : IGenericService<OrderItem>
    {
        Task<OrderItem> CreateAsync(OrderItem orderItem);
        Task<OrderItem> GetByIdAsync(int OrderID, int ProductID);
        Task UpdateAsync(OrderItem orderItem);
        void DeleteAsync(int OrderID, int ProductID);
    }
}
