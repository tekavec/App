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

        public CreditLimit GetCreditLimit(string companyName, string firstname, string surname, DateTime dateOfBirth)
        {
            if (companyName == "VeryImportantClient")
            {
                return new VeryImportantClientCreditLimit().GetCreditLimit();

            }
            else if (companyName == "ImportantClient")
            {
                return new ImportantClientCreditLimit(_creditLimitAmountService, firstname, surname, dateOfBirth).GetCreditLimit();
            }
            else
            {
                return new OtherClientCreditLimit(_creditLimitAmountService, firstname, surname, dateOfBirth).GetCreditLimit();
            }
        }
    }
}