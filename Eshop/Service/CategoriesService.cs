using Azure.Core;
using Eshop.Context;
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.CategoryModel;
using Eshop.Repositries.Interface;
using Eshop.Service.Inteterface;

namespace Eshop.Service
{

    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository categoryRepository;
        private readonly ILogger<CategoriesService> logger;
        private readonly ApplicationDbContext dbContext;

        public CategoriesService(ICategoriesRepository categoryRepository, ILogger<CategoriesService> logger, ApplicationDbContext dbContext)
        {
            this.categoryRepository = categoryRepository;
            this.logger = logger;
            this.dbContext = dbContext;
        }
        
    
        
        public async Task<BaseResponse<bool>> CreateCategory(CreateCategoryDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var category = new Categories
                {
                    Name = request.Name,
                    Description = request.Description,
                    


                };

                await categoryRepository.AddAsync(category);
                await categoryRepository.SaveChangesAsync(CancellationToken.None);

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

        public async Task<BaseResponse<CategoryDto>> GetCategoryById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await categoryRepository.GetCategoryByID(id, cancellationToken);

                if (category == null)
                {
                    return BaseResponse<CategoryDto>.FailResponse("Category not found");
                }

                var categoryDto = new CategoryDto
                {
                    Name = category.Name,
                    Description = category.Description
                };

                return BaseResponse<CategoryDto>.SuccessResponse(categoryDto, "Category retrieved successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving category by ID");
                return BaseResponse<CategoryDto>.FailResponse("An error occurred while retrieving category");
            }
        }

        public async Task<List<Categories>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllCategory();
            return categories;
        }




        public async Task<BaseResponse<bool>> UpdateCategory(Guid id, UpdateCategoryDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            try
            {

                var category = await categoryRepository.GetCategoryByID(id, cancellationToken);

                if (category == null)
                {
                    response.IsSuccess = false;
                    response.Data = false;
                    response.Message = "Product not found";
                    return response;
                }

                category.Name = request.Name;
                category.Description = request.Description;

                //product.Price = request.Price;
                //product.StockQuantity = request.StockQuantity;


                var isUpdated = await categoryRepository.UpdateCategory(category, cancellationToken);

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
        public async Task<BaseResponse<bool>> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var isDeleted = await categoryRepository.DeleteCategory(id, cancellationToken);
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
