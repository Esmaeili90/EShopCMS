using EShop_CMS.DataBase.Contracts;
using EShop_CMS.DataBase.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop_CMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("AdminOnly")]
    public class ProductGroupsController : Controller
    {
        private readonly IProductGroupRepository _groupRepo;

        public ProductGroupsController(IProductGroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
        }

        // GET: /Admin/Group/Index
        [HttpGet]
        [Route("GroupManage")]
        public IActionResult Index()
        {
            var groups = _groupRepo.GetAllGroups();
            return View(groups);
        }

        // GET: /Admin/Group/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductGroup());
        }

        // POST: /Admin/Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductGroup group)
        {
            // "products" is just the EF navigation property, not something the form fills in —
            // without this, ModelState may treat it as required and fail validation.
            ModelState.Remove("products");

            if (!ModelState.IsValid)
            {
                return View(group);
            }

            _groupRepo.AddGroup(group);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var group = _groupRepo.GetGroupByIdNoTracking(id); 
            if (group == null)
                return NotFound();

            return View(group);
        }

        // POST: /Admin/Group/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductGroup group)
        {
            ModelState.Remove("products");

            if (!ModelState.IsValid)
            {
                return View(group);
            }

            var existingGroup = _groupRepo.GetGroupById(group.Id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            _groupRepo.UpdateGroup(group);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Group/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var group = _groupRepo.GetGroupById(id);
            if (group == null)
            {
                return NotFound();
            }

            // Prevent deleting a category that still has products assigned to it —
            // otherwise this would fail with a Foreign Key constraint error,
            // or worse, silently orphan those products depending on your DB settings.
            if (group.products != null && group.products.Any())
            {
                TempData["Error"] = $"Cannot delete \"{group.GroupTitle}\" because it still has {group.products.Count} product(s) assigned to it. Move or delete those products first.";
                return RedirectToAction(nameof(Index));
            }

            _groupRepo.DeleteGroup(group);
            return RedirectToAction(nameof(Index));
        }
    }
}
