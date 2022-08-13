namespace ProductsAPI.Models
{
    public class ProductsAvailability
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string IsStockAvailable { get; set; }
    }
}
