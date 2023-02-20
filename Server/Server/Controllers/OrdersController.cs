using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        [HttpGet]
        public string CancelOrder(int id)
        {
            _orderService.CancelOrderById(id);
            return "Заказ отменен!";
        }

        [HttpPost]
        public string OrderProducts(Order order)
        {
            _orderService.OrderProducts(order);
            return "Заказ принят!";
        }

        [HttpGet]
        public IEnumerable<Order> GetAllOrders()
        {
            return _orderService.GetAllOrders();
        }
    }
}
