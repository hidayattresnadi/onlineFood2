using OnlineFoodWebAPI.Models;

namespace OnlineFoodWebAPI.Interfaces
{
    public interface ICustomerService
    {
        Customer AddCustomer(Customer customer);
        IEnumerable<Customer> GetAllCustomer();
        Customer GetCustomerById(Guid id);
        Customer UpdateCustomer(Customer customer, Guid id);
        string DeleteCustomer(Guid id);
    }
}
