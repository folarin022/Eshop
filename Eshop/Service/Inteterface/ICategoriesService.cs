
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.CategoryModel;

namespace Eshop.Service.Inteterface
{
    public interface ICategoriesService
    {
        Task<BaseResponse<bool>> CreateCategory(CreateCategoryDto request, CancellationToken cancellationToken);
        Task<BaseResponse<CategoryDto>> GetCategoryById(Guid id, CancellationToken cancellationToken);
        Task<List<Categories>> GetAllCategories();
        Task<BaseResponse<bool>> UpdateCategory(Guid id,UpdateCategoryDto request ,CancellationToken cancellationToken);
        Task<BaseResponse<bool>> DeleteCategory(Guid id, CancellationToken cancellationToken);
        //Task CreateCategory(Category category, CancellationToken cancellationToken);
        //Task UpdateCategory(Category category, CancellationToken cancellationToken);
    }
}
