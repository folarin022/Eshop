using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.CategoryModel;
using Eshop.Repositries.Interface;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Service
{
    public class CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger) : ICategoryService
    {
       

        public async Task<BaseResponse<bool>> CreateCategory(Category category, CancellationToken cancellationToken)
        {
            try
            {
                var isAdded = await categoryRepository.CreateCategory(category, cancellationToken);
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
                    Id = category.Id,
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

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllCategory();
            return categories;
        }




        public async Task<BaseResponse<bool>> UpdateCategory(Category category, CancellationToken cancellationToken)
        {
            try
            {
                await categoryRepository.UpdateCategory(category, cancellationToken);
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
