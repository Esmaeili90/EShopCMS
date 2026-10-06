using EShop_CMS.DataBase.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop_CMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class OrderManageController : Controller
    {
        private readonly IOrderRepository _ordrep;

        public OrderManageController(IOrderRepository orderRepository)
        {
            _ordrep = orderRepository;
        }

        public IActionResult Index()
        {
            var result = _ordrep.GetFinalizedOrders();
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetCompleted(int OrderId)
        {
            var order = _ordrep.GetOrderById(OrderId);
            if (order == null)
                return NotFound();

            order.IsCompleted = true;
            _ordrep.UpdateOrder(order);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder(int OrderId)
        {
            var order = _ordrep.GetOrderById(OrderId);
            if (order == null)
                return NotFound();

            order.IsFinaly = false;
            _ordrep.UpdateOrder(order);
            return RedirectToAction("Index");
        }
    }
}