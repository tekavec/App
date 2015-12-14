using System;
using App.Infrastructure.Exceptions;

namespace App.Model
{
    public class CustomerFactory : ICustomerFactory
    {
        private readonly ICustomerValidator _customerValidator;

        public CustomerFactory(ICustomerValidator customerValidator)
        {
            _customerValidator = customerValidator;
        }

        public Customer CreateCustomer(string firstname, string surname, string emailAddress, DateTime dateOfBirth, Company company, ICreditLimit creditLimit)
        {
            if (!_customerValidator.IsValid(firstname, surname, emailAddress, dateOfBirth, creditLimit.HasCreditLimit, creditLimit.CreditLimitAmount))
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