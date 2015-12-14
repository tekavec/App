using System;
using App.Infrastructure;
using App.Services;

namespace App.Model
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICreditLimitService _creditLimitService;
        private readonly ICustomerValidator _customerValidator;

        public CustomerFactory(ICompanyRepository companyRepository, ICreditLimitService creditLimitService, ICustomerValidator customerValidator)
        {
            _companyRepository = companyRepository;
            _creditLimitService = creditLimitService;
            _customerValidator = customerValidator;
        }

        public Customer CreateCustomer(string firstname, string surname, string emailAddress, DateTime dateOfBirth, int companyId)
        {
            var company = _companyRepository.GetById(companyId);
            var creditLimit = _creditLimitService.SetCreditLimitTo(company.Name, firstname, surname, dateOfBirth);
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
    }
}