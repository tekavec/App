using System;
using App.Model;
using App.Services.CreditLimitAmount;

namespace App.Services.CustomerCreditLimit
{
    public class ImportantClientCreditLimitCalculator : ICreditLimitCalculator
    {
        private readonly ICreditLimitAmountService _creditLimitAmountService;
        private readonly string _firstname;
        private readonly string _surname;
        private readonly DateTime _dateOfBirth;
        private const bool HasCreditLimit = true;
        private const int MultiplierForImportantClientsCustomer = 2;

        public ImportantClientCreditLimitCalculator(ICreditLimitAmountService creditLimitAmountService, string firstname, string surname, DateTime dateOfBirth)
        {
            _creditLimitAmountService = creditLimitAmountService;
            _firstname = firstname;
            _surname = surname;
            _dateOfBirth = dateOfBirth;
        }

        public ICreditLimit GetCreditLimit()
        {
            var creditLimitAmount = _creditLimitAmountService.GetCreditLimitAmount(_firstname, _surname, _dateOfBirth);
            return new CreditLimit(HasCreditLimit, creditLimitAmount * MultiplierForImportantClientsCustomer);
        }
    }
}