using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICartService : IGenericService<Cart>
    {
        Task<Cart> CreateAsync(Cart cart);
        Task<Cart> GetByIdAsync(Guid CartID);
        Task UpdateAsync(Guid CartID, Cart cart);
        void DeleteAsync(Guid CartID);
    }
}
