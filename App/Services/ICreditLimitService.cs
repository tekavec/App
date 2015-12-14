using System;
using App.Model;

namespace App.Services
{
    public interface ICreditLimitService
    {
        CreditLimit SetCreditLimitTo(string companyName, string firstname, string surname, DateTime dateOfBirth);
    }
}