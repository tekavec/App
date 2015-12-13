using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public class CustomerService
    {
        private readonly ICustomer _customer;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICreditLimitService _creditLimitService;

        public CustomerService()
        {
            _customer = new Customer();
            _companyRepository = new CompanyRepository();
            _customerRepository = new CustomerRepository();
            _creditLimitService = new CreditLimitService();
        }

        public CustomerService(ICustomer customer, ICustomerRepository customerRepository, ICompanyRepository companyRepository, ICreditLimitService creditLimitService)
        {
            _customer = customer;
            _customerRepository = customerRepository;
            _companyRepository = companyRepository;
            _creditLimitService = creditLimitService;
        }

        public bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            if (string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            var company = _companyRepository.GetById(companyId);

            _customer.Company = company;
            _customer.DateOfBirth = dateOfBirth;
            _customer.EmailAddress = email;
            _customer.Firstname = firname;
            _customer.Surname = surname;

            _creditLimitService.SetCreditLimitTo(_customer);

            if (_customer.HasCreditLimit && _customer.CreditLimit < 500)
            {
                return false;
            }

            _customerRepository.AddCustomer(_customer);

            return true;
        }
    }
}
