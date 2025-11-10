namespace Eshop.Dto.ProductModel
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal price { get; set; }
        public decimal CostPrice { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int StockQuantity { get; set; }
    }
}
