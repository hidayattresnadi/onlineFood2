using Microsoft.AspNetCore.Mvc;
using OnlineFoodWebAPI.Interfaces;
using OnlineFoodWebAPI.Models;
using OnlineFoodWebAPI.Services;

namespace OnlineFoodWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        /// <summary>
        /// Adds a new customer to the system.
        /// </summary>
        /// <param name="customer">The new customer to add.</param>
        /// <returns>The created customer data.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Customer
        ///     {
        ///        "Name":"dayat",
        ///        "Email":"dayat@gmail.com",
        ///        "PhoneNumber":"083234342",
        ///        "Address":"Bogor"
        ///     }
        ///     
        ///     Note: 
        ///     The Name field is required and has minimum length between 2 and 100.
        ///     The Email field is required and format should be valid.
        ///     The PhoneNumber is optional but if it is filled, the phone should be valid format.
        ///     The Address is optional but if it is filled, the address should have maximal 200 characters.
        ///     
        /// Sample response:
        /// 
        ///     {
        ///         "CustomerId": "a226a013-3c00-4aca-bd89-b3d72ddfd06e",,
        ///         "Name": "dayat888",
        ///         "Email":"dayat@gmail.com",
        ///         "PhoneNumber":"083234342",
        ///         "RegistrationDate": "2024-08-02T16:20:21.2001742+07:00"
        ///     }
        /// </remarks>
        /// <response code="200">Returns the created menu item</response>
        /// <response code="400">If the Name field is missing, the Email is invalid, the PhoneNumber and the address is invalid</response>
        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            var createdCustomer = _customerService.AddCustomer(customer);
            return Ok(createdCustomer);
        }
        /// <summary>
        /// Get all customers from the system.
        /// </summary>
        /// <returns>List of customers.</returns>
        /// <remarks>
        /// 
        /// GET /Customer
        /// 
        /// Sample response:
        /// 
        ///        [
        ///           {
        ///              "customerId": "ff862475-2c48-4eec-8eeb-dbec19ba772b",
        ///              "Name": "dayat888",
        ///              "Email":"dayat@gmail.com",
        ///              "PhoneNumber":"083234342",
        ///              "RegistrationDate": "2024-08-02T16:20:21.2001742+07:00"
        ///            }
        ///         ]
        /// </remarks>
        /// <response code="200">Returns all of the menu items that have been added</response>

        [HttpGet]
        public IActionResult GetAllCustomers() 
        {
            var customers = _customerService.GetAllCustomer();
            return Ok(customers);
        }
        /// <summary>
        /// Get a customer from customer lists based on id.
        /// </summary>
        /// <param name="id">The chosen of user.</param>
        /// <returns>The customer with id from user input.</returns>
        /// <remarks>
        /// Sample param:
        /// 
        /// "ff862475-2c48-4eec-8eeb-dbec19ba772b"
        /// 
        /// GET /Customer/{id}
        /// 
        /// Sample response:
        /// 
        ///     {
        ///         "CustomerId": "ff862475-2c48-4eec-8eeb-dbec19ba772b",
        ///         "Name": "dayat888",
        ///         "Email":"dayat@gmail.com",
        ///         "PhoneNumber":"083234342",
        ///         "RegistrationDate": "2024-08-02T16:20:21.2001742+07:00"
        ///     }
        /// </remarks>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If customer is not found</response>
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(Guid id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null) 
            {
                return NotFound();
            }
            return Ok(customer);
        }
        /// <summary>
        /// Updates an existing customer by id.
        /// </summary>
        /// <param name="customer">The customer data to update.</param>
        /// <param name="id">The chosen of customer id to update.</param>
        /// <returns>The updated customer data.</returns>
        /// <remarks>
        /// 
        /// Sample param:
        /// 
        /// "ff862475-2c48-4eec-8eeb-dbec19ba772b"
        /// 
        /// Sample request:
        /// 
        ///     PUT /Customer/{id}
        ///     {
        ///        "Name":"dayatkiki",
        ///        "Email":"dayat@gmail.com",
        ///        "PhoneNumber":"083234342",
        ///        "Address":"Bogor"
        ///     }
        ///     
        ///     Note: 
        ///     The Name field is required and has minimum length between 2 and 100.
        ///     The Email field is required and format should be valid.
        ///     The PhoneNumber is optional but if it is filled, the phone should be valid format.
        ///     The Address is optional but if it is filled, the address should have maximal 200 characters.
        ///     
        /// Sample response:
        /// 
        ///     {
        ///         "CustomerId": "ff862475-2c48-4eec-8eeb-dbec19ba772b",
        ///         "Name": "dayatkiki",
        ///         "Email":"dayat@gmail.com",
        ///         "PhoneNumber":"083234342",
        ///         "RegistrationDate": "2024-08-02T16:20:21.2001742+07:00"
        ///     }
        /// </remarks>
        /// <response code="200">Returns the created menu item</response>
        /// <response code="400">If the Name field is missing, the Price is invalid, or the Category is invalid</response>
        /// <response code="404">If the menu item is not found</response>

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer([FromBody] Customer customer,Guid id)
        {
            var updatedCustomer = _customerService.UpdateCustomer(customer, id);
            if (updatedCustomer == null)
            {
                return NotFound();
            }
            return Ok(updatedCustomer);
        }
        /// <summary>
        /// Delete a customer from customer lists based on id.
        /// </summary>
        /// <param name="id">The chosen of customer data to be deleted.</param>
        /// <returns>String message if data is deleted or not.</returns>
        /// <remarks>
        /// 
        /// Sample param:
        /// 
        /// "ff862475-2c48-4eec-8eeb-dbec19ba772b"
        /// 
        /// DELETE /Menu/{id}
        /// 
        /// Sample response: 
        /// 
        /// "Customer Terhapus"
        /// </remarks>
        /// <response code="200">Returns string : "Customer Terhapus"</response>
        /// <response code="404">If the customer is not found</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(Guid id) 
        {
            string message = _customerService.DeleteCustomer(id);
            if (message == "Customer tidak ditemukan")
            {
                return NotFound();
            }
            return Ok(message);
        }
    }
}
