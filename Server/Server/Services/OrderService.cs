using Server.Data;
using Server.Models;

namespace Server.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public void CancelOrderById(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        public void OrderProducts(Order order)
        {
            if(order == null)
            {
                throw new NullReferenceException("Заказ не может быть пустым");
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
