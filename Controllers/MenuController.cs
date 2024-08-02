using Microsoft.AspNetCore.Mvc;
using OnlineFoodWebAPI.Interfaces;
using OnlineFoodWebAPI.Models;

namespace OnlineFoodWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        /// <summary>
        /// Adds a new menu item to the system.
        /// </summary>
        /// <param name="menu">The menu item to add.</param>
        /// <returns>The created menu item.</returns>
        /// <remarks>
        /// All the parameters in request body cannot be null.
        /// Sample request:
        /// 
        ///     POST /Menu
        ///     {
        ///        "Name": "Bakso",
        ///        "Price": 10000,
        ///        "Category": "Food"
        ///     }
        ///     
        ///     Note: 
        ///     The Name field is required and has minimum length between 2 and 100.
        ///     The Price field should be between 0.01 and 100000
        ///     The Category field can only be populated with the values: "Food", "Beverage", "Dessert".
        ///     
        /// Sample response:
        /// 
        ///     {
        ///         "id": "952bc6c4-2aef-40f4-b0dc-464d305e7d6f",
        ///         "name": "Bakso",
        ///         "price": 10000,
        ///         "category": "Food",
        ///         "rating": 0,
        ///         "createdAt": "2024-08-02T15:54:28.4873094+07:00",
        ///         "isAvailable": true,
        ///         "ratingCustomers": []
        ///     }
        /// </remarks>
        /// <response code="200">Returns the created menu item</response>
        /// <response code="400">If the Name field is missing, the Price is invalid, or the Category is invalid</response>
        [HttpPost]
        public IActionResult AddMenu([FromBody] Menu menu)
        {
            var createdMenu = _menuService.AddMenu(menu);
            return Ok(createdMenu);
        }
        /// <summary>
        /// Get all menu items from the system.
        /// </summary>
        /// <returns>List of menu items.</returns>
        /// <remarks>
        /// GET /Menu
        /// 
        /// Sample response:
        /// 
        ///        [
        ///           {
        ///              "id": "f106d477-1299-45fd-9345-6bf39f161b3e",
        ///              "name": "Bakso2",
        ///              "price": 20000,
        ///              "category": "Food",
        ///              "rating": 3.5,
        ///              "createdAt": "2024-08-02T15:47:01.2529995+07:00",
        ///              "isAvailable": false,
        ///              "ratingCustomers": [4,3]
        ///            },
        ///            {
        ///               "id": "952bc6c4-2aef-40f4-b0dc-464d305e7d6f",
        ///               "name": "Bakso",
        ///               "price": 10000,
        ///               "category": "Food",
        ///               "rating": 0,
        ///               "createdAt": "2024-08-02T15:54:28.4873094+07:00",
        ///               "isAvailable": true,
        ///               "ratingCustomers": []
        ///             }
        ///         ]
        /// </remarks>
        /// <response code="200">Returns all of the menu items that have been added</response>

        [HttpGet]
        public IActionResult GetMenus()
        {
            var allMenus = _menuService.GetAllMenu();
            return Ok(allMenus);
        }
        /// <summary>
        /// Get a menu item from menu Item lists based on id.
        /// </summary>
        /// <param name="id">The chosen of menu id from user.</param>
        /// <returns>The menu item with id from user input.</returns>
        /// <remarks>
        /// Sample param:
        /// 
        /// "952bc6c4-2aef-40f4-b0dc-464d305e7d6f"
        /// 
        /// GET /Menu/{id}
        /// 
        /// Sample response:
        /// 
        ///     {
        ///         "id": "952bc6c4-2aef-40f4-b0dc-464d305e7d6f",
        ///         "name": "Bakso",
        ///         "price": 10000,
        ///         "category": "Food",
        ///         "rating": 0,
        ///         "createdAt": "2024-08-02T15:54:28.4873094+07:00",
        ///         "isAvailable": true,
        ///         "ratingCustomers": []
        ///     }
        /// </remarks>
        /// <response code="200">Returns the menu item</response>
        /// <response code="404">If the menu item is not found</response>
        [HttpGet("{id}")]
        public IActionResult GetMenuById(Guid id)
        {
            var chosenMenu = _menuService.GetMenuById(id);
            if (chosenMenu == null)
            {
                return NotFound();
            }
            return Ok(chosenMenu);
        }
        /// <summary>
        /// Updates an existing menu item by id from user.
        /// </summary>
        /// <param name="menu">The menu item to add.</param>
        /// <param name="id">The chosen of menu id from user to update.</param>
        /// <returns>The updated menu menu item.</returns>
        /// <remarks>
        /// All the parameters in request body cannot be null.
        /// 
        /// Sample param:
        /// 
        /// "952bc6c4-2aef-40f4-b0dc-464d305e7d6f"
        /// 
        /// Sample request:
        /// 
        ///     PUT /Menu/{id}
        ///     {
        ///        "Name": "Bakso Ayam",
        ///        "Price": 11000,
        ///        "Category": "Food"
        ///     }
        ///     
        ///     Note: 
        ///     The Name field is required and has minimum length between 2 and 100.
        ///     The Price field should be between 0.01 and 100000
        ///     The Category field can only be populated with the values: "Food", "Beverage", "Dessert".
        ///     
        /// Sample response:
        /// 
        ///     {
        ///         "id": "952bc6c4-2aef-40f4-b0dc-464d305e7d6f",
        ///         "name": "Bakso Ayam",
        ///         "price": 11000,
        ///         "category": "Food",
        ///         "rating": 0,
        ///         "createdAt": "2024-08-02T15:54:28.4873094+07:00",
        ///         "isAvailable": true,
        ///         "ratingCustomers": []
        ///     }
        /// </remarks>
        /// <response code="200">Returns the created menu item</response>
        /// <response code="400">If the Name field is missing, the Price is invalid, or the Category is invalid</response>
        /// <response code="404">If the menu item is not found</response>

        [HttpPut("{id}")]
        public IActionResult UpdateMenuById([FromBody] Menu menu, Guid id)
        {
            var updatedMenu = _menuService.UpdateMenu(menu, id);
            if (updatedMenu == null)
            {
                return NotFound();
            }
            return Ok(updatedMenu);
        }
        /// <summary>
        /// Delete a menu item from menu Item lists based on id.
        /// </summary>
        /// <param name="id">The chosen of menu id from user.</param>
        /// <returns>The menu item with id from user input.</returns>
        /// <remarks>
        /// 
        /// Sample param:
        /// 
        /// "952bc6c4-2aef-40f4-b0dc-464d305e7d6f"
        /// 
        /// DELETE /Menu/{id}
        /// 
        /// Sample response: 
        /// 
        /// "Menu berhasil didelete"
        /// </remarks>
        /// <response code="200">Returns string : "Menu berhasil didelete" </response>
        /// <response code="404">If the menu item is not found</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(Guid id)
        {
            string message = _menuService.DeleteMenu(id);
            if (message == "Menu tidak ditemukan")
            {
                return NotFound();
            }
            return Ok("Menu berhasil didelete");
        }
        /// <summary>
        /// Updates property Rating and RatingCustomers at existing menu item from user.
        /// </summary>
        /// <param name="id">The menu item to add rating.</param>
        /// <param name="rating">rating from user.</param>
        /// <returns>The message to shows status for updating rating</returns>
        /// <remarks>
        /// 
        /// Sample param:
        /// 
        /// "952bc6c4-2aef-40f4-b0dc-464d305e7d6f"
        /// 
        /// Sample request:
        /// 
        ///     PUT /Menu/{id}
        ///     4
        ///     
        ///     Note: 
        ///     The Rating should be between 0 and 5.
        ///     
        /// Sample response:
        /// 
        /// "berhasil menambah rating";
        /// </remarks>
        /// <response code="200">Returns string that mentions update rating is success</response>
        /// <response code="400">If rating input is more than 5 or less than 0</response>
        /// <response code="404">If the menu item is not found</response>
        [HttpPatch("{id}/rating")]
        public IActionResult AddRating(Guid id, [FromBody] int rating)
        {
            string message = _menuService.AddRating(id, rating);
            if (message == "Menu tidak ditemukan")
            {
                return NotFound();
            }
            if(message == "gagal menambah rating")
            {
                return BadRequest(message);
            }
            return Ok(message);
        }
    }
}
