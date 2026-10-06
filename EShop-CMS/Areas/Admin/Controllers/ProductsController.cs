using EShop_CMS.DataBase.Contracts;
using EShop_CMS.DataBase.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace EShop_CMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ProductsController : Controller
    {
        #region Ctor&Vars
        IProductGroupRepository _grorep;
        IProductRepository _prorep;
        private readonly IWebHostEnvironment _env;
        public ProductsController(IProductRepository ProductRep, IProductGroupRepository grorep, IWebHostEnvironment env)
        {
            _prorep = ProductRep;
            _grorep = grorep;
            _env = env;
        }
        #endregion


        [Route("ProductManage")]
        public IActionResult Index()
        {
            var result = _prorep.GetAllProducts();
            return View(result);
        }


        public IActionResult AddProduct()
        {
            ViewBag.Groups = _grorep.GetAllGroups();
            return View(new Product());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Groups = _grorep.GetAllGroups();
                return View(product);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string ext = Path.GetExtension(ImageFile.FileName).ToLower();
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                {
                    product.ImgName = Guid.NewGuid() + ext;
                    string savePath = Path.Combine(_env.WebRootPath, "ProductsImg", product.ImgName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }
                }
            }

            _prorep.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Index(string keyword)
        {
            var products = string.IsNullOrWhiteSpace(keyword)
                ? _prorep.GetAllProducts()
                : _prorep.SearchProducts(keyword);

            return View(products);
        }
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _prorep.GetProductByIdNoTracking(id);
            if (product == null)
                return NotFound();

            var groups = _grorep.GetAllGroups();
            ViewBag.Groups = new SelectList(groups, "Id", "GroupTitle", product.group);

            return View(product);
        }

        // POST: /Admin/Products/EditProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product product, IFormFile ImageFile)
        {
            // "group" is just the EF navigation property, not something the form fills in —
            // without this, ModelState treats it as required and always fails validation.
            ModelState.Remove("group");

            if (!ModelState.IsValid)
            {
                var groups = _grorep.GetAllGroups();
                ViewBag.Groups = new SelectList(groups, "Id", "GroupTitle", product.group);
                return View(product);
            }

            var existingProduct = _prorep.GetProductByIdNoTracking(product.Id);
            if (existingProduct == null)
                return NotFound();

            // Keep the old image unless the admin uploaded a new one
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string ext = Path.GetExtension(ImageFile.FileName).ToLower();
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                {
                    string folderPath = Path.Combine(_env.WebRootPath, "ProductsImg");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    product.ImgName = Guid.NewGuid() + ext;
                    string savePath = Path.Combine(folderPath, product.ImgName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }
                }
                else
                {
                    product.ImgName = existingProduct.ImgName;
                }
            }
            else
            {
                product.ImgName = existingProduct.ImgName;
            }

            _prorep.UpdateProduct(product);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteProduct(int id)
        {
            _prorep.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
