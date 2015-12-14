using System;

namespace App
{
    public class CustomerService
    {
        private readonly ICustomer _customer;
        private readonly IClock _clock;
        private readonly ICreditLimitService _creditLimitService;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService()
        {
            _customer = new Customer();
            _clock = new Clock();
            _creditLimitService = new CreditLimitService();
            _companyRepository = new CompanyRepository();
            _customerRepository = new CustomerRepository();
        }

        public CustomerService(ICustomer customer, IClock clock, ICompanyRepository companyRepository, ICreditLimitService creditLimitService, ICustomerRepository customerRepository)
        {
            _customer = customer;
            _clock = clock;
            _creditLimitService = creditLimitService;
            _companyRepository = companyRepository;
            _customerRepository = customerRepository;
        }

        public bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            if (string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            if (!email.Contains("@") || !email.Contains("."))
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
