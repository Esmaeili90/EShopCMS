using EShop_CMS.DataBase.Contracts;
using EShop_CMS.DataBase.Model;
using EShop_CMS.DataBase.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop_CMS.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;

        public CartController(IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst("UserId").Value;
        }

        // GET: /Cart/Index
        [HttpGet]
        public IActionResult Index()
        {
            var model = BuildCartViewModel(GetCurrentUserId());
            return View(model);
        }

        // POST: /Cart/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId, int quantity = 1)
        {
            var product = _productRepo.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            string currentUserId = GetCurrentUserId();
            var order = _orderRepo.GetActiveOrderByUserId(currentUserId);

            if (order == null)
            {
                order = new Order
                {
                    UserId = currentUserId,
                    CreateDate = DateTime.Now,
                    IsFinaly = false,
                    Sum = 0
                };
                _orderRepo.AddOrder(order);

                _orderRepo.AddOrderDetail(new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = productId,
                    Count = quantity,
                    Price = product.Price
                });
            }
            else
            {
                var existingDetail = _orderRepo.GetOrderDetail(order.OrderId, productId);
                if (existingDetail == null)
                {
                    _orderRepo.AddOrderDetail(new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = productId,
                        Count = quantity,
                        Price = product.Price
                    });
                }
                else
                {
                    existingDetail.Count += quantity;
                    _orderRepo.UpdateOrderDetail(existingDetail);
                }
            }

            RecalculateOrderSum(order.OrderId, currentUserId);

            return RedirectToAction("Product", "ShowProduct", new { id = productId });
        }

        // POST: /Cart/UpdateQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            string currentUserId = GetCurrentUserId();
            var order = _orderRepo.GetActiveOrderByUserId(currentUserId);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var detail = _orderRepo.GetOrderDetail(order.OrderId, productId);
            if (detail == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (quantity <= 0)
            {
                _orderRepo.RemoveOrderDetail(detail);
            }
            else
            {
                detail.Count = quantity;
                _orderRepo.UpdateOrderDetail(detail);
            }

            RecalculateOrderSum(order.OrderId, currentUserId);

            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int productId)
        {
            string currentUserId = GetCurrentUserId();
            var order = _orderRepo.GetActiveOrderByUserId(currentUserId);
            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var detail = _orderRepo.GetOrderDetail(order.OrderId, productId);
            if (detail != null)
            {
                _orderRepo.RemoveOrderDetail(detail);
            }

            RecalculateOrderSum(order.OrderId, currentUserId);

            return RedirectToAction(nameof(Index));
        }

        // ---------- helpers ----------

        private CartViewModel BuildCartViewModel(string userId)
        {
            var model = new CartViewModel();

            var order = _orderRepo.GetActiveOrderByUserIdNoTracking(userId);
            if (order == null)
            {
                return model;
            }

            var details = _orderRepo.GetOrderDetailsWithProduct(order.OrderId);

            model.Items = details.Select(d => new CartItemViewModel
            {
                ProductId = d.ProductId,
                Title = d.Product?.Title,
                ImgName = d.Product?.ImgName,
                Price = d.Price,
                Quantity = d.Count,
                IsPresent = d.Product?.IsPresent ?? false
            }).ToList();

            return model;
        }

        private void RecalculateOrderSum(int orderId, string userId)
        {
            var order = _orderRepo.GetActiveOrderByUserId(userId);
            if (order == null) return;

            var details = _orderRepo.GetOrderDetailsWithProduct(orderId);
            order.Sum = details.Sum(d => d.Count * d.Price);
            _orderRepo.UpdateOrder(order);
        }
    }
}
