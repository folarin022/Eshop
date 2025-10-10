namespace Eshop.Data
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int StockQuantity { get; set; }
    }
}
