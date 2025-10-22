using Eshop.Data;
using Eshop.Dto;
using Eshop.Dto.ProductModel;
using System.Threading;

namespace Eshop.Service.Inteterface
{
    public interface IProductsService
    {
       
        Task<BaseResponse<bool>> AddProduct(CreateProductDto request, CancellationToken cancellationToken);

        Task<BaseResponse<bool>> GetProductById(Guid id, CancellationToken cancellationToken);

        Task<BaseResponse<bool>> UpdateProduct(Guid id,  UpdateProductDto request,CancellationToken cancellationToken);
        Task<List<Products>> GetAllProduct();

        Task<BaseResponse<bool>> DeleteProduct(Guid id, CancellationToken cancellationToken);
        //Task<BaseResponse<bool>> UpdateProduct(Guid id, object request);
    }
}
    