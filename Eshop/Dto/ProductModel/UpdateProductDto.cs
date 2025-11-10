namespace Eshop.Dto.ProductModel
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public IFormFile Image { get; set; }
        public int StockQuantity { get; set; }
    }
}
