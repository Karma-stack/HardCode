using HardCode.WebAPI.Interfaces;
using HardCode.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace HardCode.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequestModel model)
        {
            var result = await _productService.CreateProductAsync(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] ProductRequestModel model)
        {
            var result = await _productService.UpdateProductAsync(productId, model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ProductsList(string? search)
        {
            var result = await _productService.GetProductsListAsync(search);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Product(int productId)
        {
            var result = await _productService.GetProductAsync(productId);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProductAsync(productId);
            return Ok(result);
        }
    }
}
