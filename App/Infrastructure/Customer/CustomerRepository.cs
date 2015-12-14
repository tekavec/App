using App.Model;

namespace App.Infrastructure.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(ICustomer customer)
        {
            CustomerDataAccess.AddCustomer(customer);
        }
    }
}