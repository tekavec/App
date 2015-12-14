using System;

namespace App.Services.CreditLimitAmount
{
    public interface ICreditLimitAmountService
    {
        int GetCreditLimitAmount(string firstname, string surname, DateTime dateOfBirth);
    }
}