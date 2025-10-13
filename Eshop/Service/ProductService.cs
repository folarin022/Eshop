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
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<ProductService> logger;
        private readonly ApplicationDbContext dbContext;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, ApplicationDbContext dbContext)
        {
            this.productRepository = productRepository;
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<BaseResponse<bool>> AddProduct(CreateProductDto request)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.price,
                    CategoryId = request.CategoryId
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



        public async Task<BaseResponse<bool>> GetProductById(Guid id)
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
        public async Task<BaseResponse<bool>> UpdateProduct(Product product, CancellationToken cancellationToken)
        {
            try
            {
                await productRepository.UpdateProduct(product, cancellationToken);
                return new BaseResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Category updated successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while updating category");
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = "An error occurred while updating the category",
                    Data = false
                };
            }
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
                        Message = "Failed to delete category",
                        Data = false
                    };
                }
                return new BaseResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Category deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting category");
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = "An error occurred while deleting the category",
                    Data = false
                };
            }
        }
    }
}
