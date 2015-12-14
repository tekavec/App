using System;
using App.Model;

namespace App.Services
{
    public interface ICreditLimitAmountService
    {
        int GetCreditLimitAmount(string firstname, string surname, DateTime dateOfBirth);
    }
}