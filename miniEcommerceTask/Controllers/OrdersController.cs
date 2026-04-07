using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using miniEcommerceTask.Data;
using miniEcommerceTask.Models;

namespace miniEcommerceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public OrdersController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpPost]
        public IActionResult Create(Order order)
        {
            decimal total = 0;
            int itemCount = 0;

            foreach (var item in order.Items)
            {
                var product = dbContext.Products.Find(item.ProductId);

                if (product == null || product.Quantity < item.Quantity)
                    return BadRequest("Stock Empty");

                item.Price = product.Price;
                total += product.Price * item.Quantity;

                product.Quantity = product.Quantity - item.Quantity;
                itemCount += item.Quantity;
            }

            decimal discount = 0;

            if (itemCount >= 2 && itemCount <= 4)
                discount = total * 0.05m;

            if (itemCount >= 5)
                discount = total * 0.10m;

            order.Discount = discount;
            order.Total = total - discount;

            dbContext.Orders.Add(order);

            dbContext.SaveChanges();

            return Ok(order);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = dbContext.Orders.Include(o => o.Items).ThenInclude(Oi => Oi.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }
}
