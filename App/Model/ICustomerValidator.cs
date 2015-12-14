using System;

namespace App.Model
{
    public interface ICustomerValidator
    {
        bool IsValid(string firstname, string surname, string emailAddress, DateTime dateOfBirth, bool hasCreditLimit, int creditLimitAmount);
    }
}