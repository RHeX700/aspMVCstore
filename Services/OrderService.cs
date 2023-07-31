using Contracts;
using Contracts.Orders;
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
    internal class OrderService : IOrderService
    {
        private IRepositoryManager _repositoryManager;

        public OrderService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<Order> CreateAsync(Order order)
        {
            return _repositoryManager.OrderRepository.Insert(order);
        }

        public async void DeleteAsync(int OrderID)
        {
            var order = await _repositoryManager.OrderRepository.GetByIdAsync(OrderID);
            _repositoryManager.OrderRepository.Delete(order);
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return _repositoryManager.OrderRepository.GetAllAsync();
        }

        public Task<IEnumerable<Order>> GetAllAsync(string OwnerID)
        {
            return _repositoryManager.OrderRepository.GetAllAsync(OwnerID);
        }

        public Task<Order> GetByIdAsync(int OrderID)
        {
            return _repositoryManager.OrderRepository.GetByIdAsync(OrderID);
        }

        public Task<Order> UpdateAsync(Order order)
        {
            return _repositoryManager.OrderRepository.Update(order);
        }
    }
}
