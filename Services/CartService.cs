using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Carts;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    internal class CartService : ICartService
    {
        private IRepositoryManager _repositoryManager;

        public CartService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<Cart> CreateAsync(Cart cart)
        {
            return _repositoryManager.CartRepository.Insert(cart);
        }

        public async void DeleteAsync(Guid CartID)
        {
            var cart = await _repositoryManager.CartRepository.GetByIdAsync(CartID);
            _repositoryManager.CartRepository.Delete(cart);
        }

        public Task<IEnumerable<Cart>> GetAllAsync()
        {
            return _repositoryManager.CartRepository.GetAllAsync();
        }

        public Task<Cart> GetByIdAsync(Guid CartID)
        {
            return _repositoryManager.CartRepository.GetByIdAsync(CartID);
;        }

        public Task UpdateAsync(Guid CartID, Cart cart)
        {
            return _repositoryManager.CartRepository.Update(cart);
        }
    }
}
