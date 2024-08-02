using System.ComponentModel.DataAnnotations;

namespace OnlineFoodWebAPI.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        [AllowedValues("Processed", "Delivered", "Cancelled", ErrorMessage = "Only 3 statuses are allowed :Processed, Delivered, and Cancelled")]
        public string OrderStatus { get; set; } = "Processed";
        public string Note { get; set; }
        public List<Guid> MenuId { get; set; }
        public List<Menu> MenuItems { get; set; } = new List<Menu>();

        public void CalculatedTotalOrder()
        {
            decimal total = 0;
            foreach (var menuItem in MenuItems)
            {
                total += menuItem.Price;
            }
            TotalPrice = total;
        }
    }
}
