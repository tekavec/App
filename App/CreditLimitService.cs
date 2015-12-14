using System;

namespace App
{
    public class CreditLimitService : ICreditLimitService
    {
        private readonly ICreditLimitAmountService _creditLimitAmountService;

        public CreditLimitService(ICreditLimitAmountService creditLimitAmountService)
        {
            _creditLimitAmountService = creditLimitAmountService;
        }

        public void SetCreditLimitTo(ICustomer customer)
        {
            if (customer.Company.Name == "VeryImportantClient")
            {
                // Skip credit check
                customer.HasCreditLimit = false;
            }
            else if (customer.Company.Name == "ImportantClient")
            {
                // Do credit check and double credit limit
                customer.HasCreditLimit = true;
                var creditLimit = _creditLimitAmountService.GetCreditLimitAmount(customer);
                customer.CreditLimit = creditLimit*2;
            }
            else
            {
                // Do credit check
                customer.HasCreditLimit = true;
                var creditLimit = _creditLimitAmountService.GetCreditLimitAmount(customer);
                customer.CreditLimit = creditLimit;
            }
        }
    }
}