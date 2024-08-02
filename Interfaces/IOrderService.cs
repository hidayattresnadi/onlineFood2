using OnlineFoodWebAPI.Models;

namespace OnlineFoodWebAPI.Interfaces
{
    public interface IOrderService
    {
        Order PlaceOrder(Order order);
        Order DisplayOrderDetails(Guid id);
        string CancelOrder(Guid id);
        string UpdateOrderStatus(Guid id, string orderStatus);
        string GetOrderStatus(Guid id);
    }
}
