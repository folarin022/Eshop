using Eshop.Context;
using Eshop.Data;
using Eshop.Dto.CategoryModel;
using Eshop.Repositries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Repositries
{
    public class CategoriesRepositries(ApplicationDbContext dbContext) : ICategoriesRepository
    {
        public async Task<bool> CreateCategory(Categories category, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(category, cancellationToken);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            dbContext.Remove(id);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<Categories>> GetAllCategory()
        {
            return await dbContext.Categories.ToListAsync();

        }

        public async Task<Categories> GetCategoryByID(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<bool> UpdateCategory(Categories category, CancellationToken cancellationToken)
        {
            dbContext.Categories.Update(category);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task AddAsync(Categories category , CancellationToken cancellationToken = default)
        {
            await dbContext.Categories.AddAsync(category, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
