using System;
using App.Services.CreditLimitAmount;

namespace App.Services.CustomerCreditLimit
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
            if (companyName == "ImportantClient")
            {
                return new ImportantClientCreditLimit(_creditLimitAmountService, firstname, surname, dateOfBirth);
            }
            return new OtherClientCreditLimit(_creditLimitAmountService, firstname, surname, dateOfBirth);
        }
    }
}