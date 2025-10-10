
using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.CategoryModel;

namespace Eshop.Service.Inteterface
{
    public interface ICategoryService
    {
        Task<BaseResponse<bool>> CreateCategory(Data.Category category, CancellationToken cancellationToken);
        Task<BaseResponse<CategoryDto>> GetCategoryById(Guid id, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> UpdateCategory(Data.Category category, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> DeleteCategory(Guid id, CancellationToken cancellationToken);
        //Task CreateCategory(Category category, CancellationToken cancellationToken);
        //Task UpdateCategory(Category category, CancellationToken cancellationToken);
    }
}
