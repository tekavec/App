using System;
using App.Infrastructure;
using App.Infrastructure.Company;
using App.Infrastructure.Exceptions;
using App.Services;
using App.Services.CustomerCreditLimit;

namespace App.Model
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICreditLimitCalculatorFactory _creditLimitCalculatorFactory;
        private readonly ICustomerValidator _customerValidator;

        public CustomerFactory(ICompanyRepository companyRepository, ICreditLimitCalculatorFactory creditLimitCalculatorFactory, ICustomerValidator customerValidator)
        {
            _companyRepository = companyRepository;
            _creditLimitCalculatorFactory = creditLimitCalculatorFactory;
            _customerValidator = customerValidator;
        }

        public Customer CreateCustomer(string firstname, string surname, string emailAddress, DateTime dateOfBirth, int companyId)
        {
            var company = _companyRepository.GetById(companyId);
            var creditLimit = GetCreditLimit(firstname, surname, dateOfBirth, company);
            if (!_customerValidator.IsValid(firstname, surname, emailAddress, dateOfBirth,creditLimit.HasCreditLimit, creditLimit.CreditLimitAmount))
            {
                throw new CreatingCustomerNotAllowedException();
            }

            var customer = new Customer
                                {
                                    Company = company,
                                    DateOfBirth = dateOfBirth,
                                    EmailAddress = emailAddress,
                                    Firstname = firstname,
                                    Surname = surname,
                                    HasCreditLimit = creditLimit.HasCreditLimit,
                                    CreditLimit = creditLimit.CreditLimitAmount
                                };
            return customer;
        }

        private CreditLimit GetCreditLimit(string firstname, string surname, DateTime dateOfBirth, Company company)
        {
            var creditLimitCalculator = _creditLimitCalculatorFactory.GetCreditLimitCalculator(company.Name, firstname, surname, dateOfBirth);
            return creditLimitCalculator.GetCreditLimit();
        }
    }
}