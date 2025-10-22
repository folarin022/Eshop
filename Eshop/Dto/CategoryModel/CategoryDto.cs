using Eshop.Dto.ProductModel;

namespace Eshop.Dto.CategoryModel
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    
     public static implicit operator CategoryDto(Task<BaseResponse<ProductDto>> v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator CategoryDto(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
