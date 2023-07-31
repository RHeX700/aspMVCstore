using Contracts.OrderItems;
using Contracts.Products;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task<OrderItem> GetByIdAsync(int OrderID, int ProductID);
        Task<OrderItem> Insert(OrderItem entity);
        Task<OrderItem> Update(OrderItem entity);
        void Remove(OrderItem entity);
    }
}
