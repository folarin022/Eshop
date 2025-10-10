namespace Eshop.Dto.ProductModel
{
    public class CreateProductDto
    {
        public decimal price { get; set; }
        public decimal CostPrice { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public int StockQuantity { get; set; }
    }
}
