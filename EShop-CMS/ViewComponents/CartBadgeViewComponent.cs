using EShop_CMS.DataBase.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EShop_CMS.ViewComponents
{
    public class CartBadgeViewComponent : ViewComponent
    {
        private readonly IOrderRepository _orderRepo;

        public CartBadgeViewComponent(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public IViewComponentResult Invoke()
        {
            if (!(UserClaimsPrincipal?.Identity?.IsAuthenticated ?? false))
            {
                return Content(string.Empty);
            }

            var userId = UserClaimsPrincipal.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Content(string.Empty);
            }

            var order = _orderRepo.GetActiveOrderByUserIdNoTracking(userId);
            int itemCount = 0;

            if (order != null)
            {
                var details = _orderRepo.GetOrderDetailsWithProduct(order.OrderId);
                itemCount = details.Sum(d => d.Count);
            }
            

            return View("CartBadge", itemCount);
        }
    }
}