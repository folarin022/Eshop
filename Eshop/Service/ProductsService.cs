using Eshop.Context;
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.CategoryModel;
using Eshop.Dto.ProductModel;
using Eshop.Repositries;
using Eshop.Repositries.Interface;
using Eshop.Service.Inteterface;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Service
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository productRepository;
        private readonly ILogger<ProductsService> logger;
        private readonly ApplicationDbContext dbContext;

        public ProductsService(IProductsRepository productRepository, ILogger<ProductsService> logger, ApplicationDbContext dbContext)
        {
            this.productRepository = productRepository;
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<BaseResponse<bool>> AddProduct(CreateProductDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var product = new Products
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.price,
                    CostPrice = request.CostPrice,
                    StockQuantity = request.StockQuantity,


                };

                await productRepository.AddAsync(product);
                await productRepository.SaveChangesAsync();

                response.IsSuccess = true; 
                response.Data = true;
                response.Message = "Product created successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error creating product: {ex.Message}";
            }

            return response;
        }



        public async Task<BaseResponse<bool>> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await productRepository.GetProductByID(id, CancellationToken.None);
                return new BaseResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Product retrieved successfully",

                    Data = true
                };

            }
            catch
            {
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving the category",
                    Data = false
                };
            }
        }
        public async Task<BaseResponse<bool>> UpdateProduct(Guid id, UpdateProductDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
               
                var product = await productRepository.GetProductByID(id, cancellationToken);

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
                product.StockQuantity = request.StockQuantity;

                
                var isUpdated = await productRepository.UpdateProduct(product, cancellationToken);

                response.IsSuccess = isUpdated;
                response.Data = isUpdated;
                response.Message = isUpdated
                    ? "Product updated successfully"
                    : "Failed to update product";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Data = false;
                response.Message = $"Error updating product: {ex.Message}";
            }

            return response;
        }


        public async Task<BaseResponse<bool>> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var isDeleted = await productRepository.DeleteProduct(id, cancellationToken);
                if (!isDeleted)
                {
                    return new BaseResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Failed to delete Product",
                        Data = false
                    };
                }
                return new BaseResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Product deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting product");
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = "An error occurred while deleting the product",
                    Data = false
                };
            }
        }

        public async Task<List<Products>> GetAllProduct()
        {
            var product = await productRepository.GetAllProduct();
            return product;
        }
    }
}
