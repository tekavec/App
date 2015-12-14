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
        private readonly ICompanyRepository _companyRepository;
        private readonly ICreditLimitCalculatorFactory _creditLimitCalculatorFactory;

        public CustomerService()
        {
            _creditLimitCalculatorFactory = new CreditLimitCalculatorFactory(new CreditLimitAmountService());
            _companyRepository = new CompanyRepository();
            _customerRepository = new CustomerRepository();
            _customerFactory = new CustomerFactory(new CustomerValidator(new Clock()));
            _creditLimitCalculatorFactory = new CreditLimitCalculatorFactory(new CreditLimitAmountService());
        }

        public CustomerService(ICustomerRepository customerRepository, ICustomerFactory customerFactory, ICompanyRepository companyRepository, ICreditLimitCalculatorFactory creditLimitCalculatorFactory)
        {
            _customerFactory = customerFactory;
            _customerRepository = customerRepository;
            _companyRepository = companyRepository;
            _creditLimitCalculatorFactory = creditLimitCalculatorFactory;
        }

        public bool AddCustomer(string firstname, string surname, string emailAddress, DateTime dateOfBirth, int companyId)
        {
            try
            {
                var company = _companyRepository.GetById(companyId);
                var creditLimit = GetCreditLimit(firstname, surname, dateOfBirth, company);
                var customer = _customerFactory.CreateCustomer(firstname, surname, emailAddress, dateOfBirth, company, creditLimit);
                _customerRepository.AddCustomer(customer);
                return true;
            }
            catch (CreatingCustomerNotAllowedException)
            {
                return false;
            }
        }

        private ICreditLimit GetCreditLimit(string firstname, string surname, DateTime dateOfBirth, Company company)
        {
            var creditLimitCalculator = _creditLimitCalculatorFactory.GetCreditLimitCalculator(company.Name, firstname, surname, dateOfBirth);
            return creditLimitCalculator.GetCreditLimit();
        }
    }
}
