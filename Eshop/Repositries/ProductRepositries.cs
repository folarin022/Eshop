using Eshop.Context;
using Eshop.Data;
using Eshop.Repositries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Repositries
{
    public class ProductRepositries(ApplicationDbContext dbContext) : IProductRepository
    {
        public async Task<bool> AddProduct(Product Product, CancellationToken cancellationToken)
        {
            await dbContext.AddAsync(Product, cancellationToken);
            return await dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            dbContext.Remove(id);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<Product>> GetAllProduct(CancellationToken cancellationToken)
        {
            return await dbContext.Products.ToListAsync(cancellationToken);
        }

        public async Task<Product> GetProductByID(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task UpdateProduct(Product Product, CancellationToken cancellationToken)
        {
            dbContext.Products.Update(Product);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
