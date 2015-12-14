using System;
using App.Model;

namespace App.Services
{
    public interface ICreditLimitCalculatorFactory
    {
        ICreditLimitCalculator GetCreditLimitCalculator(string companyName, string firstname, string surname, DateTime dateOfBirth);
    }
}