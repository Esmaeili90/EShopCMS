using EShop_CMS.DataBase.Model;

namespace EShop_CMS.DataBase.Contracts
{
    public interface IOrderRepository
    {
        // Order
        int AddOrder(Order order);
        void UpdateOrder(Order order);
        Order GetActiveOrderByUserId(string userId);
        Order GetActiveOrderByUserIdNoTracking(string userId);

        // OrderDetail
        void AddOrderDetail(OrderDetail detail);
        void UpdateOrderDetail(OrderDetail detail);
        void RemoveOrderDetail(OrderDetail detail);
        void RemoveOrderDetailById(int orderDetailId);
        OrderDetail GetOrderDetail(int orderId, int productId);
        OrderDetail GetOrderDetailById(int orderDetailId);
        List<OrderDetail> GetOrderDetailsWithProduct(int orderId);
        List<Order> GetFinalizedOrders();
        Order GetOrderById(int orderId);
    }
}
