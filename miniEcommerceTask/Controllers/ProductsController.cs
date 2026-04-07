using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniEcommerceTask.Data;
using miniEcommerceTask.Models;

namespace miniEcommerceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product.Price < 0 || product.Quantity < 0)
                return BadRequest("Invalid");

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return Ok(product);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var products = dbContext.Products.ToList();
            return Ok(products);
        }


    }
}
