using Eshop.Data;
using Eshop.Dto.ProductModel;

namespace Eshop.Repositries.Interface
{
    public interface IProductsRepository
    {
        Task<List<Products>> GetAllProduct();
        Task<Products> GetProductByID(Guid id, CancellationToken cancellationToken);
        Task<bool> AddProduct(CreateProductDto request);
        Task<bool> UpdateProduct(Products product, CancellationToken cancellationToken);
        Task<bool> DeleteProduct(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Products product, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

