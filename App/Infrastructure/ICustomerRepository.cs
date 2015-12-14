using App.Model;

namespace App.Infrastructure
{
    public interface ICustomerRepository
    {
        void AddCustomer(ICustomer customer);
    }
}