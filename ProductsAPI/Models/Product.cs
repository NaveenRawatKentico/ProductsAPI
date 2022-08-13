namespace ProductsAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public int StockAvailable { get; set; }


    }
}
