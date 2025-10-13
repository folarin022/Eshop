using Eshop.Data;
using Eshop.Dto.ProductModel;

namespace Eshop.Repositries.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProduct(CancellationToken cancellationToken);
        Task<Product> GetProductByID(Guid id, CancellationToken cancellationToken);
        Task<bool> AddProduct(CreateProductDto request);
        Task UpdateProduct(Product Product, CancellationToken cancellationToken);
        Task<bool> DeleteProduct(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

