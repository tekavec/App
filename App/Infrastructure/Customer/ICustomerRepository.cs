using App.Model;

namespace App.Infrastructure.Customer
{
    public interface ICustomerRepository
    {
        void AddCustomer(ICustomer customer);
    }
}