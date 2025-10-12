using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.CategoryModel;
using Eshop.Dto.ProductModel;
using Eshop.Repositries.Interface;
using Eshop.Service.Inteterface;

namespace Eshop.Service
{
    public class ProductService(IProductRepository productRepository, ILogger<ProductService> logger) : IProductService
    {
        public async Task<BaseResponse<bool>> AddProduct(CreateProductDto request)
        {
            try
            {
                var isAdded = await productRepository.AddProduct(request);
                if (!isAdded)
                {
                    return new BaseResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Failed to add category",
                        Data = false
                    };
                }
                return new BaseResponse<bool>
                {
                    IsSuccess = true,
                    Message = "Category added successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while adding category");
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Message = "An error occurred while adding the category",
                    Data = false
                };
            }
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
