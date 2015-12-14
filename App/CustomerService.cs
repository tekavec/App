using System;
using App.Infrastructure.Clock;
using App.Infrastructure.Company;
using App.Infrastructure.Customer;
using App.Infrastructure.Exceptions;
using App.Model;
using App.Services.CreditLimitAmount;
using App.Services.CustomerCreditLimit;

namespace App
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerFactory _customerFactory;

        public CustomerService()
        {
            ICustomerValidator customerValidator = new CustomerValidator(new Clock());
            ICreditLimitCalculatorFactory creditLimitCalculatorFactory = new CreditLimitCalculatorFactory(new CreditLimitAmountService());
            ICompanyRepository companyRepository = new CompanyRepository();
            _customerRepository = new CustomerRepository();
            _customerFactory = new CustomerFactory(companyRepository, creditLimitCalculatorFactory, customerValidator);
        }

        public CustomerService(ICustomerRepository customerRepository, ICustomerFactory customerFactory)
        {
            _customerFactory = customerFactory;
            _customerRepository = customerRepository;
        }

        public bool AddCustomer(string firstname, string surname, string emailAddress, DateTime dateOfBirth, int companyId)
        {
            try
            {
                var customer = _customerFactory.CreateCustomer(firstname, surname, emailAddress, dateOfBirth, companyId);
                _customerRepository.AddCustomer(customer);
                return true;
            }
            catch (CreatingCustomerNotAllowedException)
            {
                return false;
            }
        }
    }
}
