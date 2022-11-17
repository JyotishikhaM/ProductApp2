using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductDbContext _productDbContext;

        public ProductController(IProductRepository productRepository,ProductDbContext productDbContext)
        {
            _productRepository = productRepository;
            _productDbContext = productDbContext;
        }

        public IActionResult Index()
        {
            var obj=_productRepository.AllProducts.OrderByDescending(c => c.ProductId);
            return View(obj);
        }
        public IActionResult Details(int id)
        {
            var obj = _productRepository.GetProductById(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }
    }
}
