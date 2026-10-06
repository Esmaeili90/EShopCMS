using EShop_CMS.DataBase.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EShop_CMS.Controllers
{
    public class ShowProductController : Controller
    {
        private readonly IProductRepository _prorep;
        public ShowProductController(IProductRepository _productRepository)
        {
            _prorep = _productRepository;
        }
        [HttpGet]
        public IActionResult Product(int id)
        {
            var product = _prorep.GetProductByIdWithGroup(id);
            if (product == null)
                return NotFound();

            return View(product);
        }
        [HttpGet]
        public IActionResult Search(string keyword)
        {
            var products = string.IsNullOrWhiteSpace(keyword)
                ? _prorep.GetAllProducts()
                : _prorep.SearchProducts(keyword);

            ViewBag.Keyword = keyword;

            return View("Search", products);
        }

    }
}
