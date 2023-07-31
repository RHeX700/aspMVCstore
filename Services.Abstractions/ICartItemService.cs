using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICartItemService : IGenericService<CartItem>
    {
        Task<CartItem> CreateAsync(CartItem cartItem);
        Task<CartItem> GetByIdAsync(Guid CartID, int ProductID);
        Task<CartItem> UpdateAsync( CartItem cartItem);
        void DeleteAsync(Guid CartID, int ProductID);

        bool CartItemExists(Guid CartID, int ProductID);
    }
}
