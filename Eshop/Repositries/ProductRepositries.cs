using Eshop.Context;
using Eshop.Data;
using Eshop.Dto.ProductModel;
using Eshop.Repositries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Repositries
{
    public class ProductRepositries(ApplicationDbContext dbContext) : IProductRepository
    {
        public async Task<bool> AddProduct(CreateProductDto request)
        {
            await dbContext.AddAsync(request);
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

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await dbContext.Products.AddAsync(product, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        //public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        //{
        //    await dbContext.Products.AddAsync(product, cancellationToken);
        //}

        //public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    return await dbContext.SaveChangesAsync(cancellationToken);
        //}
    }
}
