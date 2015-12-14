using System;
using App.Model;

namespace App.Services
{
    public class ImportantClientCreditLimit : ICreditLimitCalculator
    {
        private readonly ICreditLimitAmountService _creditLimitAmountService;
        private readonly string _firstname;
        private readonly string _surname;
        private readonly DateTime _dateOfBirth;
        private const bool HasCreditLimit = true;

        public ImportantClientCreditLimit(ICreditLimitAmountService creditLimitAmountService, string firstname, string surname, DateTime dateOfBirth)
        {
            _creditLimitAmountService = creditLimitAmountService;
            _firstname = firstname;
            _surname = surname;
            _dateOfBirth = dateOfBirth;
        }

        public CreditLimit GetCreditLimit()
        {
            var creditLimitAmount = _creditLimitAmountService.GetCreditLimitAmount(_firstname, _surname, _dateOfBirth);
            return new CreditLimit(HasCreditLimit, creditLimitAmount * 2);
        }
    }
}