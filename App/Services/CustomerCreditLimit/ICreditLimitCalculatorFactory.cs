using System;

namespace App.Services.CustomerCreditLimit
{
    public interface ICreditLimitCalculatorFactory
    {
        ICreditLimitCalculator GetCreditLimitCalculator(string companyName, string firstname, string surname, DateTime dateOfBirth);
    }
}