using System;
using App.Model;

namespace App.Services
{
    public class CreditLimitCalculatorFactory : ICreditLimitCalculatorFactory
    {
        private readonly ICreditLimitAmountService _creditLimitAmountService;

        public CreditLimitCalculatorFactory(ICreditLimitAmountService creditLimitAmountService)
        {
            _creditLimitAmountService = creditLimitAmountService;
        }

        public ICreditLimitCalculator GetCreditLimitCalculator(string companyName, string firstname, string surname, DateTime dateOfBirth)
        {
            if (companyName == "VeryImportantClient")
            {
                return new VeryImportantClientCreditLimit();

            }
            else if (companyName == "ImportantClient")
            {
                return new ImportantClientCreditLimit(_creditLimitAmountService, firstname, surname, dateOfBirth);
            }
            else
            {
                return new OtherClientCreditLimit(_creditLimitAmountService, firstname, surname, dateOfBirth);
            }
        }
    }
}