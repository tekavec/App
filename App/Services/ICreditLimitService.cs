using System;
using App.Model;

namespace App.Services
{
    public interface ICreditLimitService
    {
        CreditLimit GetCreditLimit(string companyName, string firstname, string surname, DateTime dateOfBirth);
    }
}