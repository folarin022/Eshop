using Eshop.Dto.ProductModel;

namespace Eshop.Dto.CategoryModel
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
     
    }
}
