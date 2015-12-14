using System;
using App.Infrastructure.Clock;

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
            if (!AreFirstnameAndSurnameValid(firstname, surname)) return false;
            if (!IsEmailAddressValid(emailAddress)) return false;
            if (!IsAgeValid(dateOfBirth)) return false;
            if (!IsCreditLimitSufficient(hasCreditLimit, creditLimitAmount)) return false;
            return true;
        }
        private bool AreFirstnameAndSurnameValid(string firstname, string surname)
        {
            return !string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(surname);
        }
        private bool IsEmailAddressValid(string emailAddress)
        {
            return emailAddress.Contains("@") && emailAddress.Contains(".");
        }

        private bool IsAgeValid(DateTime dateOfBirth)
        {
            var now = _clock.Now();
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age >= 21;
        }

        private bool IsCreditLimitSufficient(bool hasCreditLimit, int creditLimitAmount)
        {
            return !hasCreditLimit || creditLimitAmount >= 500;
        }
    }
}