using OnlineFoodWebAPI.Interfaces;
using OnlineFoodWebAPI.Models;

namespace OnlineFoodWebAPI.Services
{
    public class OrderService : IOrderService
    {
        private static List<Order> Orders = new List<Order>();
        private readonly IMenuService _menuService;
        public OrderService(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public Order PlaceOrder(Order order)
        {
            var newOrder = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = order.CustomerId,
                OrderDate = DateTime.Now,
                Note = order.Note,
                MenuId = order.MenuId
            };
            List<Menu> menuItems = new List<Menu>();

            foreach (var menu in order.MenuId)
            {
                Menu chosenMenu = _menuService.GetMenuById(menu);
                if (chosenMenu == null)
                {
                    throw new KeyNotFoundException($"Menu with ID {menu} not found");
                }
                menuItems.Add(chosenMenu);
            }
            newOrder.MenuItems = menuItems;
            newOrder.CalculatedTotalOrder();
            Orders.Add(newOrder);
            return newOrder;
        }
        public Order DisplayOrderDetails(Guid id)
        {
            Order order = Orders.FirstOrDefault(foundOrder => foundOrder.Id == id);
            return order;
        }
        public string CancelOrder(Guid id)
        {
            int indexOrder = Orders.FindIndex(filteredOrder => filteredOrder.Id == id);
            if (indexOrder == -1)
            {
                return "Order tidak ditemukan";
            }
            if (Orders[indexOrder].OrderStatus == "Delivered" || Orders[indexOrder].OrderStatus == "Canceled")
            {
                return "Order tidak bisa dicancel";
            }
            Orders[indexOrder].OrderStatus = "Canceled";
            return "Order Tercancel";
        }
        public string UpdateOrderStatus(Guid id, string orderStatus)
        {
            int indexOrder = Orders.FindIndex(filteredOrder => filteredOrder.Id == id);
            if (indexOrder == -1)
            {
                return "Order tidak ditemukan";
            }
            if(Orders[indexOrder].OrderStatus == "Delivered" || Orders[indexOrder].OrderStatus == "Canceled")
            {
                return "Order status tidak bisa diubah";
            }
            Orders[indexOrder].OrderStatus = orderStatus;
            return "Order sedang diantar";
        }
        public string GetOrderStatus(Guid id)
        {
            Order order = Orders.FirstOrDefault(foundOrder => foundOrder.Id == id);
            if (order == null) 
            {
                return "Order tidak ditemukan";
            }
            return order.OrderStatus;
        }
    }
}
