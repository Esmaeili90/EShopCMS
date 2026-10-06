using EShop_CMS.DataBase.Context;
using EShop_CMS.DataBase.Contracts;
using EShop_CMS.DataBase.Model;
using Microsoft.EntityFrameworkCore;

namespace EShop_CMS.DataBase.Repository
{
    public class OrderRepository : IOrderRepository
    {
        DefaultContext _context;
        public OrderRepository(DefaultContext context)
        {
            _context = context;
        }

        // ---------- Order ----------

        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.OrderId;
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public Order GetActiveOrderByUserId(string userId)
        {
            return _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
        }

        // Use this version when you only need to read (e.g. building the cart
        // summary for display) so it doesn't collide with a tracked instance
        // used elsewhere in the same request.
        public Order GetActiveOrderByUserIdNoTracking(string userId)
        {
            return _context.Orders.AsNoTracking().FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
        }

        // ---------- OrderDetail ----------

        public void AddOrderDetail(OrderDetail detail)
        {
            _context.OrderDetails.Add(detail);
            _context.SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetail detail)
        {
            _context.OrderDetails.Update(detail);
            _context.SaveChanges();
        }

        public void RemoveOrderDetail(OrderDetail detail)
        {
            _context.OrderDetails.Remove(detail);
            _context.SaveChanges();
        }

        public void RemoveOrderDetailById(int orderDetailId)
        {
            var detail = _context.OrderDetails.Find(orderDetailId);
            if (detail != null)
            {
                _context.OrderDetails.Remove(detail);
                _context.SaveChanges();
            }
        }

        public OrderDetail GetOrderDetail(int orderId, int productId)
        {
            return _context.OrderDetails
                .FirstOrDefault(d => d.OrderId == orderId && d.ProductId == productId);
        }

        public OrderDetail GetOrderDetailById(int orderDetailId)
        {
            return _context.OrderDetails.Find(orderDetailId);
        }

        public List<OrderDetail> GetOrderDetailsWithProduct(int orderId)
        {
            return _context.OrderDetails
                .Include(d => d.Product)
                .Where(d => d.OrderId == orderId)
                .ToList();
        }
        public List<Order> GetFinalizedOrders()
        {
            return _context.Orders.Where(o => o.IsFinaly).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }
    }
}
