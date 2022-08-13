using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly ProductsAPIDbContext dbContext;

        public ProductsController(ProductsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await dbContext.Products.Select(x => new
            {
                x.Id,
                x.ProductID,
                x.ProductName
            }).OrderBy(x=>x.ProductName).ToListAsync());
        }

        [HttpPost]
        [Route("CheckOrderFullfillment")]
        public IActionResult CheckOrderFullfillment(Guid ProductId)
        {
            Product product = dbContext.Products.Where(s => s.ProductID.CompareTo(ProductId) == 0 && s.StockAvailable > 0).FirstOrDefault();
            if (product != null)
            {
                return Ok(new ProductsAvailability
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    IsStockAvailable = "Yes"
                });
            }
            else
            {
                return Ok(new ProductsAvailability
                {
                    IsStockAvailable = "No"
                });
            }
        }

        [HttpPost]
        [Route("SearchProduct")]
        public IActionResult SearchProduct(string ProductName)
        {
            if (!string.IsNullOrWhiteSpace(ProductName))
            {
                return Ok(dbContext.Products.Where(s => s.ProductName.Contains(ProductName)).ToList());
            }
            else
            {
                return NotFound();
            }
        }
    }
}
