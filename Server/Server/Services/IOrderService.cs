using Server.Models;

namespace Server.Services
{
    public interface IOrderService
    {
        void OrderProducts(Order order);
        void CancelOrderById(int id);
        IEnumerable<Order> GetAllOrders();
    }
}
