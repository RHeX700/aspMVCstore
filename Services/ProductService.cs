using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Contracts.Products;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    internal class ProductService : IProductService
    {
        private IRepositoryManager _repositoryManager;

        public ProductService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<Product> CreateAsync(Product product)
        {
            return _repositoryManager.ProductRepository.Insert(product);
        }

        public async void DeleteAsync(int ProductID)
        {
            var product = await _repositoryManager.ProductRepository.GetByIdAsync(ProductID);
            _repositoryManager.ProductRepository.Delete(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _repositoryManager.ProductRepository.GetAllAsync();
        }

        public Task<IQueryable<Product>> GetAllAsync(string? searchString)
        {
            return _repositoryManager.ProductRepository.GetAllAsync(searchString);
        }

        public Task<Product> GetByIdAsync(int ProductID)
        {
            return _repositoryManager.ProductRepository.GetByIdAsync(ProductID);
        }

        public Task<Product> UpdateAsync(Product product)
        {
            return _repositoryManager.ProductRepository.Update(product);
        }
    }
}
