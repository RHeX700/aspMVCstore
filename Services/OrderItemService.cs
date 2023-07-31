using Contracts;
using Contracts.OrderItems;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class OrderItemService : IOrderItemService
    {
        private IRepositoryManager _repositoryManager;

        public OrderItemService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<OrderItem> CreateAsync(OrderItem orderItem)
        {
            return _repositoryManager.OrderItemRepository.Insert(orderItem);
        }

        public async void DeleteAsync(int OrderID, int ProductID)
        {
            var item = await _repositoryManager.OrderItemRepository.GetByIdAsync(OrderID, ProductID);
            _repositoryManager.OrderItemRepository.Remove(item);
        }

        public Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return _repositoryManager.OrderItemRepository.GetAllAsync();
        }

        public Task<OrderItem> GetByIdAsync(int OrderID, int ProductID)
        {
            return _repositoryManager.OrderItemRepository.GetByIdAsync(OrderID, ProductID);
        }

        public Task UpdateAsync(OrderItem orderItem)
        {
            return _repositoryManager.OrderItemRepository.Update(orderItem);
        }
    }
}
