using Microsoft.AspNetCore.Mvc;
using OnlineFoodWebAPI.Interfaces;
using OnlineFoodWebAPI.Models;
using OnlineFoodWebAPI.Services;

namespace OnlineFoodWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController :ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public OrderController(ICustomerService customerService,IOrderService orderService) {
            _customerService = customerService;
            _orderService = orderService;
        }
        /// <summary>
        /// Adds a new order to the system.
        /// </summary>
        /// <param name="order">The new order to add.</param>
        /// <returns>The created order data.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Order
        ///     {
        ///        "CustomerId":"ff862475-2c48-4eec-8eeb-dbec19ba772b",
        ///        "Note":"jangan pedes ya",
        ///        "MenuId":["f106d477-1299-45fd-9345-6bf39f161b3e","952bc6c4-2aef-40f4-b0dc-464d305e7d6f"]
        ///     }
        ///     
        ///     Note: 
        ///     The CustomerId should be filled.
        ///     The MenuId should be valid.
        ///     
        /// Sample response:
///        {
///    "id": "2aacc9d4-f526-4163-adb7-82c4c6f1892b",
///    "customerId": "ff862475-2c48-4eec-8eeb-dbec19ba772b",
///    "orderDate": "2024-08-02T15:54:54.4236837+07:00",
///    "totalPrice": 30000,
///    "orderStatus": "Processed",
///    "note": "jangan pedes ya",
///    "menuId": [
///        "f106d477-1299-45fd-9345-6bf39f161b3e",
///        "952bc6c4-2aef-40f4-b0dc-464d305e7d6f"
///    ],
///    "menuItems": [
///        {
///            "id": "f106d477-1299-45fd-9345-6bf39f161b3e",
///            "name": "Bakso2",
///            "price": 20000,
///            "category": "Food",
///            "rating": 3.5,
///           "createdAt": "2024-08-02T15:47:01.2529995+07:00",
///            "isAvailable": false,
///            "ratingCustomers": [
///                4,
///                3
///            ]
///    },
///        {
///            "id": "952bc6c4-2aef-40f4-b0dc-464d305e7d6f",
///            "name": "Bakso",
///            "price": 10000,
///            "category": "Food",
///            "rating": 0,
///            "createdAt": "2024-08-02T15:54:28.4873094+07:00",
///            "isAvailable": true,
///            "ratingCustomers": []
///}
///    ]
///}
        /// </remarks>
        /// <response code="200">Returns the created order</response>
        /// <response code="400">If customer is not found, the Email menu is not found</response>

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            try
            {
                Customer registeredCustomer = _customerService.GetCustomerById(order.CustomerId);
                if (registeredCustomer == null)
                {
                    return BadRequest("Customer tidak ditemukan");
                }
                Order placedOrder = _orderService.PlaceOrder(order);
                return Ok(placedOrder);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Get an order based on id.
        /// </summary>
        /// <param name="id">The chosen of order id from user.</param>
        /// <returns>The order item with id from user input.</returns>
        /// <remarks>
        /// Sample param:
        /// 
        /// "2aacc9d4-f526-4163-adb7-82c4c6f1892b"
        /// 
        /// GET /Order/{id}
        /// 
        /// Sample response:
        /// 
        ///        {
        ///    "id": "2aacc9d4-f526-4163-adb7-82c4c6f1892b",
        ///    "customerId": "ff862475-2c48-4eec-8eeb-dbec19ba772b",
        ///    "orderDate": "2024-08-02T15:54:54.4236837+07:00",
        ///    "totalPrice": 30000,
        ///    "orderStatus": "Processed",
        ///    "note": "jangan pedes ya",
        ///    "menuId": [
        ///        "f106d477-1299-45fd-9345-6bf39f161b3e",
        ///        "952bc6c4-2aef-40f4-b0dc-464d305e7d6f"
        ///    ],
        ///    "menuItems": [
        ///        {
        ///            "id": "f106d477-1299-45fd-9345-6bf39f161b3e",
        ///            "name": "Bakso2",
        ///            "price": 20000,
        ///            "category": "Food",
        ///            "rating": 3.5,
        ///           "createdAt": "2024-08-02T15:47:01.2529995+07:00",
        ///            "isAvailable": false,
        ///            "ratingCustomers": [
        ///                4,
        ///                3
        ///            ]
        ///    },
        ///        {
        ///            "id": "952bc6c4-2aef-40f4-b0dc-464d305e7d6f",
        ///            "name": "Bakso",
        ///            "price": 10000,
        ///            "category": "Food",
        ///            "rating": 0,
        ///            "createdAt": "2024-08-02T15:54:28.4873094+07:00",
        ///            "isAvailable": true,
        ///            "ratingCustomers": []
        ///}
        ///    ]
        ///}
        /// </remarks>
        /// <response code="200">Returns the order item</response>
        /// <response code="404">If the order is not found</response>
        [HttpGet("{id}")]

        public IActionResult DisplayOrderDetail(Guid id)
        {
            Order chosenOrder = _orderService.DisplayOrderDetails(id);
            if (chosenOrder == null) 
            {  
                return NotFound(); 
            }
            return Ok(chosenOrder);
        }
        /// <summary>
        /// Update an order to status "Cancelled".
        /// </summary>
        /// <param name="id">The chosen of order id from user.</param>
        /// <returns>String message if order can be cancelled.</returns>
        /// <remarks>
        /// Sample param:
        /// 
        /// "2aacc9d4-f526-4163-adb7-82c4c6f1892b"
        /// 
        /// PATCH /Order/{id}/cancelOrder
        /// 
        /// Sample response:
        /// 
        /// "Order sudah dicancel"
        /// </remarks>
        /// <response code="200">Returns string</response>
        /// <response code="404">If the order is not found</response>
        /// <response code="400">If the order status is not "Processed"</response>
        [HttpPatch("{id}/cancelOrder")]
        public IActionResult CancelOrder(Guid id) 
        {
            string message = _orderService.CancelOrder(id);
            if (message == "Order tidak ditemukan")
            {
                return NotFound();
            }
            else if(message == "Order tidak bisa dicancel")
            {
                return BadRequest(message);
            }
            return Ok("Order sudah dicancel");
        }
        /// <summary>
        /// Update an order to status "Cancelled".
        /// </summary>
        /// <param name="id">The chosen of order id from user.</param>
        /// <param name="orderStatus">The chosen of order status from user.</param>
        /// <returns>String message if order status can be changed.</returns>
        /// <remarks>
        /// Sample param:
        /// 
        /// "2aacc9d4-f526-4163-adb7-82c4c6f1892b"
        /// 
        /// PATCH /Order/{id}/updateOrderStatus
        /// 
        /// Sample response:
        /// 
        /// "Order status sudah berubah"
        /// </remarks>
        /// <response code="200">Returns string</response>
        /// <response code="404">If the order is not found</response>
        /// <response code="400">If the order status is not "Processed"</response>
        [HttpPatch("{id}/updateOrderStatus")]
        public IActionResult UpdateOrderStatus(Guid id, [FromBody] string orderStatus)
        {
            string message = _orderService.UpdateOrderStatus(id, orderStatus);
            if (message == "Order tidak ditemukan")
            {
                return NotFound();
            }
            if(message == "Order status tidak bisa diubah")
            {
                return BadRequest(message);
            }
            return Ok("Order status sudah berubah");
        }
        /// <summary>
        /// Get order status by id order.
        /// </summary>
        /// <param name="id">The chosen of order id from user.</param>
        /// <returns>String message if order can be cancelled.</returns>
        /// <remarks>
        /// Sample param:
        /// 
        /// "2aacc9d4-f526-4163-adb7-82c4c6f1892b"
        /// 
        /// PATCH /Order/{id}/updateOrderStatus
        /// 
        /// Sample response:
        /// 
        /// "Order status sudah berubah"
        /// </remarks>
        /// <response code="200">Returns string</response>
        /// <response code="404">If the order is not found</response>
        [HttpGet("{id}/OrderStatus")]
        public IActionResult GetOrderStatus(Guid id)
        {
            string message = _orderService.GetOrderStatus(id);
            if (message == "Order tidak ditemukan")
            {
                return NotFound();
            }
            return Ok(message);
        }
    }
}
