namespace App
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(ICustomer customer)
        {
            CustomerDataAccess.AddCustomer(customer);
        }
    }
}