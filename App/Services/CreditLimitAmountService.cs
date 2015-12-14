using System;
using App.Infrastructure;
using App.Model;

namespace App.Services
{
    public class CreditLimitAmountService : ICreditLimitAmountService
    {
        public int GetCreditLimitAmount(string firstname, string surname, DateTime dateOfBirth)
        {
            int creditLimitAmount;
            using (var customerCreditService = new CustomerCreditServiceClient())
            {
                creditLimitAmount = customerCreditService.GetCreditLimit(firstname, surname, dateOfBirth);
            }
            return creditLimitAmount;
        }
    }
}