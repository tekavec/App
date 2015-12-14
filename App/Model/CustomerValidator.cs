using System;
using App.Infrastructure;

namespace App.Model
{
    public class CustomerValidator : ICustomerValidator
    {
        private readonly IClock _clock;

        public CustomerValidator(IClock clock)
        {
            _clock = clock;
        }

        public bool IsValid(string firstname, string surname, string emailAddress, DateTime dateOfBirth, bool hasCreditLimit, int creditLimitAmount)
        {
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!emailAddress.Contains("@") || !emailAddress.Contains("."))
            {
                return false;
            }

            var now = _clock.Now();
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }
            if (hasCreditLimit && creditLimitAmount < 500)
            {
                return false;
            }
            return true;
        }
    }
}