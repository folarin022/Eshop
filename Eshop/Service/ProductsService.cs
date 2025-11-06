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

        public ProductsService(
            IProductsRepository productRepositries,
            ILogger<ProductsService> logger,
            ApplicationDbContext dbContext)
        {
            _productRepositries = productRepositries;
            _logger = logger;
            _dbContext = dbContext;
        }


        public async Task<BaseResponse<bool>> AddProduct(CreateProductDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                _logger.LogInformation("Attempting to Add product with: {name}", request.Name);

                var product = new Products
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.price,
                    CostPrice = request.CostPrice,
                    StockQuantity = request.StockQuantity,


                };

                await _productRepositries.AddAsync(product);
                _logger.LogInformation("product {name} created successfully", product.Name);

                response.IsSuccess = true; 
                response.Data = true;
                response.Message = "Product created successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                _logger.LogError(ex, "Error occurred while adding product with name {name}", request.Name);
                response.Message = $"Error creating product: {ex.Message}";
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

                _logger.LogInformation("Client {ClientId} retrieved successfully", id);

                response.IsSuccess = true;
                response.Data = product;
                response.Message = "Client retrieved successfully";
            }
            catch
            {
                return new BaseResponse<Products>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving the category",
                    Data = null
                };
            }

            return response;
        }
        public async Task<BaseResponse<bool>> UpdateProduct(Guid id, UpdateProductDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                _logger.LogInformation("Attempting to update product with ID: {productId}", id);
                var product = await _productRepositries.GetProductByID(id, cancellationToken);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found for update", id);
                    response.IsSuccess = false;
                    response.Message = "Product not found";
                    return response;
                }

                product.Name = request.Name;
                    product.Description = request.Description;
                    product.Price = request.Price;
                    product.CostPrice = request.CostPrice;
                    product.StockQuantity = request.StockQuantity;

                await _productRepositries.UpdateProduct(product, cancellationToken);

                _logger.LogInformation("Product {Product} updated successfully", id);

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "Product updated successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Product with ID {Product}", id);
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
                _logger.LogInformation("Attempting to delete product with ID: {ProductId}", id);

                var isDeleted = await _productRepositries.DeleteProduct(id, cancellationToken);
                if (!isDeleted)
                {
                    _logger.LogWarning("Failed to delete product with ID: {ProductId}", id);
                    response.IsSuccess = false;
                    response.Data = false;
                    response.Message = "Failed to delete product";
                    return response;
                }

                _logger.LogInformation("Product {Product} deleted successfully", id);

                response.IsSuccess = true;
                response.Data = true;
                response.Message = "Product deleted successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product {ProductId}", id);
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error deleting product: {ex.Message}";
            }

            return response;
        }
        

        public async Task<List<Products>> GetAllProduct()
        {
            var product = await _productRepositries.GetAllProduct();
            return product;
        }
    }
}
