using Microsoft.AspNetCore.Mvc;
using miniEcommerceTask.MVC.Models;


namespace miniEcommerceTask.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _client;

        public ProductsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient();
            _client.BaseAddress = new Uri("https://localhost:7118/");
        }
        public async Task<IActionResult> Index()
        {
            var products = await _client.GetFromJsonAsync<List<Product>>("api/products");
            return View(products);
        }
    }
}
