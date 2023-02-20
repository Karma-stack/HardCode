using HardCode.WebAPI.Entities;
using HardCode.WebAPI.Interfaces;
using HardCode.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HardCode.WebAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private IAsyncRepository<Category> _categories;

        public CategoryService(IAsyncRepository<Category> categories)
        {
            _categories = categories;
        }

        public async Task<ResponseModel<Category>> GetCategoryAsync(int categoryId)
        {
            CheckId(categoryId);

            var category = await _categories.GetByIdAsync(categoryId);

            CheckModel(category);

            return new ResponseModel<Category>()
            {
                Data = category,
                Success = true
            };
        }

        public async Task<ResponseModel<List<Category>>> GetCategoriesListAsync(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var result = await _categories.Query()
                    .Where(category => category.Id.ToString().Contains(search)
                    || category.Name.Contains(search)).ToListAsync();

                return new ResponseModel<List<Category>>()
                {
                    Data = result,
                    Success = true
                };
            }

            var categories = await _categories.ListAllAsync();

            return new ResponseModel<List<Category>>()
            {
                Data = categories.ToList(),
                Success = true
            };
        }

        public async Task<ResponseModel<Category>> CreateCategoryAsync(CategoryRequestModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Заполните поля");
            }

            var result = await _categories.AddAsync(new Category { Name = model.Name });

            return new ResponseModel<Category>()
            {
                Data = result,
                Success = true
            };
        }

        public async Task<ResponseModel<Category>> UpdateCategoryAsync(int categoryId, CategoryRequestModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentNullException("Заполните поля");
            }

            CheckId(categoryId);

            var category = await _categories.GetByIdAsync(categoryId);

            CheckModel(category);

            category.Name = model.Name;

            await _categories.UpdateAsync(category);

            return new ResponseModel<Category>()
            {
                Data = category,
                Success = true
            };
        }

        public async Task<ResponseModel<Category>> DeleteCategoryAsync(int categoryId)
        {
            CheckId(categoryId);

            var category = await _categories.GetByIdAsync(categoryId);

            CheckModel(category);

            await _categories.DeleteAsync(category);

            return new ResponseModel<Category>()
            {
                Data = category,
                Success = true
            };
        }

        private bool CheckId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Нужно выбрать ID");
            }

            return true;
        }

        private bool CheckModel(Category category)
        {
            if (category == null)
            {
                throw new ArgumentException("Не найдена категория");
            }

            return true;
        }
    }
}
