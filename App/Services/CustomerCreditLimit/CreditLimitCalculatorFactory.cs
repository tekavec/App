using System;
using App.Services.CreditLimitAmount;

namespace App.Services.CustomerCreditLimit
{
    public class CreditLimitCalculatorFactory : ICreditLimitCalculatorFactory
    {
        private const string VeryImportantClientName = "VeryImportantClient";
        private const string ImportantClientName = "ImportantClient";
        private readonly ICreditLimitAmountService _creditLimitAmountService;

        public CreditLimitCalculatorFactory(ICreditLimitAmountService creditLimitAmountService)
        {
            _creditLimitAmountService = creditLimitAmountService;
        }

        public ICreditLimitCalculator GetCreditLimitCalculator(string companyName, string firstname, string surname, DateTime dateOfBirth)
        {
            if (companyName == VeryImportantClientName)
            {
                return new VeryImportantClientCreditLimitCalculator();
            }
            if (companyName == ImportantClientName)
            {
                return new ImportantClientCreditLimitCalculator(_creditLimitAmountService, firstname, surname, dateOfBirth);
            }
            return new OtherClientCreditLimitCalculator(_creditLimitAmountService, firstname, surname, dateOfBirth);
        }
    }
}