using OnlineFoodWebAPI.Interfaces;
using OnlineFoodWebAPI.Models;
using System;

namespace OnlineFoodWebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private static List<Customer> Customers = new List<Customer>();
        public Customer AddCustomer(Customer customer)
        {
            var newCustomer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                RegistrationDate = DateTime.Now,
            };
            Customers.Add(newCustomer);
            return newCustomer;
        }
        public IEnumerable<Customer> GetAllCustomer()
        {
            return Customers;
        }
        public Customer GetCustomerById(Guid id)
        {
            Customer customer = Customers.FirstOrDefault(foundCustomer => foundCustomer.CustomerId == id);
            return customer;
        }
        public Customer UpdateCustomer(Customer updatedCustomer, Guid id)
        {
            int indexCustomer = Customers.FindIndex(filteredCustomer => filteredCustomer.CustomerId == id);
            if (indexCustomer == -1)
            {
                return null;
            }
            Customers[indexCustomer].Name = updatedCustomer.Name;
            Customers[indexCustomer].Email = updatedCustomer.Email;
            Customers[indexCustomer].PhoneNumber = updatedCustomer.PhoneNumber;
            Customers[indexCustomer].Address = updatedCustomer.Address;
            return Customers[indexCustomer];
        }
        public string DeleteCustomer(Guid id)
        {
            int indexCustomer = Customers.FindIndex(filteredCustomer => filteredCustomer.CustomerId == id);
            if (indexCustomer == -1)
            {
                return "Customer tidak ditemukan";
            }
            Customers.RemoveAt(indexCustomer);
            return "Customer Terhapus";
        }
    }
}
