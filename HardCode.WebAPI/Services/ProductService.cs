using HardCode.WebAPI.Entities;
using HardCode.WebAPI.Interfaces;
using HardCode.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HardCode.WebAPI.Services
{
    public class ProductService : IProductService
    {
        private IAsyncRepository<Product> _products;
        private IAsyncRepository<Image> _images;

        public ProductService(IAsyncRepository<Product> products, IAsyncRepository<Image> images)
        {
            _products = products;
            _images = images;
        }

        public async Task<ResponseModel<Product>> GetProductAsync(int productId)
        {
            CheckId(productId);

            var product = await _products.GetByIdAsync(productId);

            CheckModel(product);

            Image image = await GetImageAsync(product.ImageId);
            product.Image = image;

            return new ResponseModel<Product>()
            {
                Data = product,
                Success = true
            };
        }

        public async Task<ResponseModel<ProductRequestModel>> CreateProductAsync(ProductRequestModel model)
        {
            if (model is null)
                throw new ArgumentNullException();

            var image = await CreateImageAsync(model.Image);

            await _products.AddAsync(new Product()
            {
                Name = model?.Name,
                CategoryId = model?.CategoryId ?? 0,
                ImageId = image.Id,
                AdditionalFields = model?.AdditionalFields,
                Description = model?.Description,
                Price = model.Price,
                Image = image
            });

            return new ResponseModel<ProductRequestModel>()
            {
                Data = model,
                Success = true
            };
        }

        public async Task<ResponseModel<List<Product>>> GetProductsListAsync(string? search)
        {
            List<Product> products = new List<Product>();
            if (!string.IsNullOrEmpty(search))
            {
                products = await _products.Query()
                    .Where(product => product.Id.ToString().Contains(search)
                    || product.Name.Contains(search)
                    || product.Price.ToString().Contains(search)
                    || product.Description.Contains(search)).ToListAsync();
            }
            else
            {
                products = _products.ListAllAsync().GetAwaiter().GetResult().ToList();
            }

            foreach (var product in products)
            {
                if (product.ImageId != 0) product.Image = await GetImageAsync(product.ImageId);
            }

            return new ResponseModel<List<Product>>()
            {
                Data = products.ToList(),
                Success = true
            };
        }

        public async Task<ResponseModel<Product>> UpdateProductAsync(int productId, ProductRequestModel model)
        {
            CheckId(productId);

            if (model is null)
            {
                throw new ArgumentNullException();
            }

            var product = await _products.GetByIdAsync(productId);

            CheckModel(product);

            Image image = new Image();

            if (model.ImageChange)
            {
                image = await CreateImageAsync(model.Image);
            }

            product.Price = model.Price!;
            product.CategoryId = model.CategoryId!;
            product.Name = model!.Name;
            product.Description = model.Description;
            product.ImageId = image.Id;
            product.Image = image;

            return new ResponseModel<Product>()
            {
                Data = product,
                Success = true
            };
        }

        public async Task<ResponseModel<Product>> DeleteProductAsync(int productId)
        {
            CheckId(productId);

            var product = await _products.GetByIdAsync(productId);

            CheckModel(product);

            await _products.DeleteAsync(product);

            return new ResponseModel<Product>()
            {
                Data = product,
                Success = true
            };
        }

        private async Task<Image> CreateImageAsync(IFormFile formFile)
        {
            byte[]? fileBytes = null;

            using (var target = new MemoryStream())
            {
                formFile?.CopyTo(target);
                fileBytes = target.ToArray();
            }

            var image = await _images.AddAsync(new Image()
            {
                Name = formFile?.FileName,
                ImageString = "data:image/" + formFile?.ContentType + ";base64," + Convert.ToBase64String(fileBytes)
            });

            return image;
        }

        private async Task<Image> GetImageAsync(int imageId)
        {
            var image = await _images.GetByIdAsync(imageId);

            if (image is null) return new Image();

            return image;
        }

        private bool CheckId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Нужно выбрать ID");
            }

            return true;
        }

        private bool CheckModel(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException("Не найдена категория");
            }

            return true;
        }
    }
}
