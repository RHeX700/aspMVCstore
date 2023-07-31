using Contracts;
using Contracts.Categories;
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
    internal class CategoryService : ICategoryService
    {
        private IRepositoryManager _repositoryManager;

        public CategoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<Category> CreateAsync(Category category)
        {
            return _repositoryManager.CategoryRepository.Insert(category);
        }

        public async void DeleteAsync(int CategoryID)
        {
            var category = await _repositoryManager.CategoryRepository.GetByIdAsync(CategoryID);
            await _repositoryManager.CategoryRepository.Insert(category);
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return _repositoryManager.CategoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int CategoryID)
        {
            return await _repositoryManager.CategoryRepository.GetByIdAsync(CategoryID);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await _repositoryManager.CategoryRepository.Update(category);
        }
    }
}
