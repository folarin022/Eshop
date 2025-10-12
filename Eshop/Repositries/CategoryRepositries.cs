using Eshop.Context;
using Eshop.Data;
using Eshop.Repositries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Repositries
{
    public class CategoryRepositries(ApplicationDbContext dbContext) : ICategoryRepository
    {
        public async Task<bool> CreateCategory(Category category, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(category, cancellationToken);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            dbContext.Remove(id);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<Category>> GetAllCategory()
        {
            return await dbContext.Categories.ToListAsync();

        }

        public async Task<Category> GetCategoryByID(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task UpdateCategory(Category category, CancellationToken cancellationToken)
        {
            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
