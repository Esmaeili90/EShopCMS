using EShop_CMS.Areas.Admin.Models;
using EShop_CMS.DataBase.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace EShop_CMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy ="AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductGroupRepository _groupRepo;
        private readonly IUserRepository _userRepo;

        public AdminController(
            IProductRepository productRepo,
            IProductGroupRepository groupRepo,
            IUserRepository userRepo)
        {
            _productRepo = productRepo;
            _groupRepo = groupRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        [Route("Admin")]
        public IActionResult Index()
        {
            var allProducts = _productRepo.GetAllProducts();
            var allGroups = _groupRepo.GetAllGroups();
            var allUsers = _userRepo.GetAllUsers();

            var model = new DashboardViewModel
            {
                TotalProducts = allProducts.Count,
                OutOfStockCount = allProducts.Count(p => !p.IsPresent),
                TotalGroups = allGroups.Count,
                TotalUsers = allUsers.Count,
                RecentProducts = allProducts
                    .OrderByDescending(p => p.DataCreated)
                    .Take(5)
                    .ToList(),
                Groups = allGroups
            };

            return View(model);
        }
    }
}
