using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.CategoryModel;
using Eshop.Dto.ProductModel;

namespace Eshop.Service.Inteterface
{
    public interface IProductService
    {
        Task<BaseResponse<bool>> AddProduct(CreateProductDto request);
        //Task CreateProduct(Product product, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> GetProductById(Guid id);
        Task<BaseResponse<bool>> UpdateProduct(Data.Product product, CancellationToken cancellationToken);
        //Task UpdateProduct(Product product, CancellationToken cancellationToken);
        Task<BaseResponse<bool>> DeleteProduct(Guid id, CancellationToken cancellationToken);

    }
}
