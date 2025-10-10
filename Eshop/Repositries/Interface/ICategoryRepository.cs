using Eshop.Data;

namespace Eshop.Repositries.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategory(CancellationToken cancellationToken);
        Task<Category> GetCategoryByID(Guid id, CancellationToken cancellationToken);
        Task<bool> CreateCategory(Category category, CancellationToken cancellationToken);
        Task UpdateCategory(Category Product, CancellationToken cancellationToken);
        Task<bool> DeleteCategory(Guid id, CancellationToken cancellationToken);
    }
}
