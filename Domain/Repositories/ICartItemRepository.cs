using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.CartItems;
using Contracts.Products;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<CartItem> GetByIdAsync(Guid CartID, int ProductID);
        Task<CartItem> Insert(CartItem entity);
        Task<CartItem> Update(CartItem entity);
        void Remove(CartItem entity);
        bool CartItemExists(Guid CartID, int ProductID);
    }
}
