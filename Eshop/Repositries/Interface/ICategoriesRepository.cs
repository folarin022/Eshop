using Eshop.Data;
using Eshop.Dto.CategoryModel;

namespace Eshop.Repositries.Interface
{
    public interface ICategoriesRepository
    {
        Task<List<Categories>> GetAllCategory();
        Task<Categories> GetCategoryByID(Guid id, CancellationToken cancellationToken);
        Task<bool> CreateCategory(Categories category, CancellationToken cancellationToken);
        Task<bool> UpdateCategory(Categories category,  CancellationToken cancellationToken);
        Task<bool> DeleteCategory(Guid id, CancellationToken cancellationToken);

        Task AddAsync(Categories category, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
