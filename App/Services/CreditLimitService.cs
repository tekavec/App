using System;
using App.Model;

namespace App.Services
{
    public class CreditLimitService : ICreditLimitService
    {
        private readonly ICreditLimitAmountService _creditLimitAmountService;

        public CreditLimitService(ICreditLimitAmountService creditLimitAmountService)
        {
            _creditLimitAmountService = creditLimitAmountService;
        }

        public CreditLimit SetCreditLimitTo(string companyName, string firstname, string surname, DateTime dateOfBirth)
        {
            if (companyName == "VeryImportantClient")
            {
                // Skip credit check
                return new CreditLimit(false, new int());

            }
            else if (companyName == "ImportantClient")
            {
                // Do credit check and double credit limit
                var creditLimit = _creditLimitAmountService.GetCreditLimitAmount(firstname, surname, dateOfBirth);
                return new CreditLimit(true, creditLimit * 2);

            }
            else
            {
                // Do credit check
                var creditLimit = _creditLimitAmountService.GetCreditLimitAmount(firstname, surname, dateOfBirth);
                return new CreditLimit(true, creditLimit);
            }
        }
    }
}