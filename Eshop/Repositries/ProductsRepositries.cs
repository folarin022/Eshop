using Eshop.Context;
using Eshop.Data;
using Eshop.Dto.ProductModel;
using Eshop.Repositries.Interface;
using Microsoft.EntityFrameworkCore;
using System;
namespace Eshop.Repositries
{
    public class ProductsRepositries(ApplicationDbContext dbContext) : IProductsRepository
    {
        public async Task<bool> AddProduct(CreateProductDto request)
        {
            await dbContext.AddAsync(request);
            return await dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(new object[] { id }, cancellationToken);
            if (product == null)
                return false;
            dbContext.Products.Remove(product);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<Products>> GetAllProduct()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Products> GetProductByID(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<bool> UpdateProduct(Products product, CancellationToken cancellationToken)
        {
            dbContext.Products.Update(product);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         ;
            
        }

        public async Task AddAsync(Products product, CancellationToken cancellationToken = default)
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
