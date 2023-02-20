using HardCode.WebAPI.Entities;
using HardCode.WebAPI.Interfaces;
using HardCode.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HardCode.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequestModel model)
        {
            var result = await _categoryService.CreateCategoryAsync(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int categoryId, CategoryRequestModel model)
        {
            var result = await _categoryService.UpdateCategoryAsync(categoryId, model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> CategoriesList(string? search)
        {
            var result = await _categoryService.GetCategoriesListAsync(search);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Category(int categoryId)
        {
            var result = await _categoryService.GetCategoryAsync(categoryId);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int categoryId)
        {
            var result = await _categoryService.DeleteCategoryAsync(categoryId);
            return Ok(result);
        }
    }
}
