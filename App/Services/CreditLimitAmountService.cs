using App.Model;

namespace App.Services
{
    public class CreditLimitAmountService : ICreditLimitAmountService
    {
        public int GetCreditLimitAmount(ICustomer customer)
        {
            int creditLimit;
            using (var customerCreditService = new CustomerCreditServiceClient())
            {
                creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
            }
            return creditLimit;
        }
    }
}