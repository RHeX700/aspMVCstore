using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class CartItemService : ICartItemService
    {
        private IRepositoryManager _repositoryManager;

        public CartItemService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<CartItem> CreateAsync(CartItem cartItem)
        {
            return _repositoryManager.CartItemRepository.Insert(cartItem);
        }

        public async void DeleteAsync(Guid CartID, int ProductID)
        {
            var item = await _repositoryManager.CartItemRepository.GetByIdAsync(CartID, ProductID);
            _repositoryManager.CartItemRepository.Remove(item);
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _repositoryManager.CartItemRepository.GetAllAsync();
        }

        public async Task<CartItem> GetByIdAsync(Guid CartID, int ProductID)
        {
            return await _repositoryManager.CartItemRepository.GetByIdAsync(CartID, ProductID);
        }

        public async Task<CartItem> UpdateAsync(CartItem cartItem)
        {
            return await _repositoryManager.CartItemRepository.Update(cartItem);
        }

        public bool CartItemExists(Guid CartID, int ProductID)
        {
            return _repositoryManager.CartItemRepository.CartItemExists(CartID, ProductID);
        }
    }
}
