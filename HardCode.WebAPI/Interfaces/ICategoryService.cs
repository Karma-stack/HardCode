using HardCode.WebAPI.Entities;
using HardCode.WebAPI.Models;

namespace HardCode.WebAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseModel<Category>> CreateCategoryAsync(CategoryRequestModel model);

        Task<ResponseModel<Category>> UpdateCategoryAsync(int categoryId, CategoryRequestModel model);

        Task<ResponseModel<Category>> DeleteCategoryAsync(int categoryId);
        Task<ResponseModel<List<Category>>> GetCategoriesListAsync(string search);
        Task<ResponseModel<Category>> GetCategoryAsync(int categoryId);
    }
}
