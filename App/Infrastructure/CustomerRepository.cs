using App.Model;

namespace App.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(ICustomer customer)
        {
            CustomerDataAccess.AddCustomer(customer);
        }
    }
}