using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Eshop.Context;
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.ProductModel;
using Eshop.Repositries.Interface;
using Eshop.Service.Inteterface;

namespace Eshop.Service
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productRepositries;
        private readonly ILogger<ProductsService> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly PhotoService _photoService;

        public ProductsService(
            IProductsRepository productRepositries,
            ILogger<ProductsService> logger,
            ApplicationDbContext dbContext,
            PhotoService photoService
        )
        {
            _productRepositries = productRepositries;
            _logger = logger;
            _dbContext = dbContext;
            _photoService = photoService;
        }

        public async Task<BaseResponse<Products>> AddProduct(CreateProductDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Products>();

            try
            {
                var product = new Products
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.price
                };

                if (request.Image != null)
                {
                    product.ImageUrl = await _photoService.UploadImageAsync(request.Image);
                }

                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync(cancellationToken);

                response.IsSuccess = true;
                response.Data = product;
                response.Message = "Product added successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product");
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"Error adding product: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<Products>> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Products>();

            try
            {
                _logger.LogInformation("Fetching product with ID: {productId}", id);

                var product = await _productRepositries.GetProductByID(id, cancellationToken);

                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found", id);
                    response.IsSuccess = false;
                    response.Message = "Product not found";
                    return response;
                }

                response.IsSuccess = true;
                response.Data = product;
                response.Message = "Product retrieved successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product with ID: {ProductId}", id);
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"Error retrieving product: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<bool>> UpdateProduct(Guid id, UpdateProductDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var product = await _productRepositries.GetProductByID(id, cancellationToken);
                if (product == null)
                {
                    response.IsSuccess = false;
                    response.Data = false;
                    response.Message = "Product not found";
                    return response;
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.CostPrice = request.CostPrice;
                product.StockQuantity = request.StockQuantity;

                if (request.Image != null)
                {
                    product.ImageUrl = await _photoService.UploadImageAsync(request.Image);
                }

                await _productRepositries.UpdateProduct(product, cancellationToken);

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "Product updated successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID {ProductId}", id);
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error updating product: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var isDeleted = await _productRepositries.DeleteProduct(id, cancellationToken);
                if (!isDeleted)
                {
                    response.IsSuccess = false;
                    response.Data = false;
                    response.Message = "Failed to delete product";
                    return response;
                }

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "Product deleted successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {ProductId}", id);
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error deleting product: {ex.Message}";
            }

            return response;
        }

        public async Task<BaseResponse<List<Products>>> GetAllProduct()
        {
            var response = new BaseResponse<List<Products>>();

            try
            {
                var products = await _productRepositries.GetAllProduct();
                response.IsSuccess = true;
                response.Data = products;
                response.Message = "Products retrieved successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = null;
                response.Message = $"Error fetching products: {ex.Message}";
            }

            return response;
        }

    }
}
