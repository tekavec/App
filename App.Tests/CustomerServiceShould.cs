using System;
using System.Runtime.InteropServices;
using NSubstitute;
using NUnit.Framework;

namespace App.Tests
{
    [TestFixture]
    public class CustomerServiceShould
    {
        private ICustomer _customer;
        private IClock _clock;
        private ICustomerRepository _customerRepository;
        private ICreditLimitService _creditLimitService;
        private ICompanyRepository _companyRepository;
        private const string EmptyString = "";
        private const string AName = "name";
        private const string AnEmail = "email@noemail.net";
        private const int ACompanyId = 1;
        private readonly DateTime _aDateOfBirth = DateTime.MinValue;

        [SetUp]
        public void Init()
        {
            _customer = Substitute.For<ICustomer>();
            _clock = new Clock();
            _companyRepository = Substitute.For<ICompanyRepository>();
            _creditLimitService = Substitute.For<ICreditLimitService>();
            _customerRepository = Substitute.For<ICustomerRepository>();
        }

        [TestCase(null, AName)]
        [TestCase(EmptyString, AName)]
        [TestCase(AName, null)]
        [TestCase(AName, EmptyString)]
        public void not_store_a_customer_if_firstname_or_surname_are_invalid_and_report_negative_result(string firstname, string surname)
        {
            var customerService = new CustomerService(_customer, _clock, _companyRepository, _creditLimitService, _customerRepository);

            var result = customerService.AddCustomer(firstname, surname, AnEmail, _aDateOfBirth, ACompanyId);

            _customerRepository.DidNotReceive().AddCustomer(_customer);
            Assert.IsFalse(result);
        }

        [TestCase("incorrect.email")]
        [TestCase("incorrect@email")]
        [TestCase("incorrectemail")]
        public void not_store_a_customer_if_email_is_invalid_and_report_negative_result(string email)
        {
            var customerService = new CustomerService(_customer, _clock, _companyRepository, _creditLimitService, _customerRepository);

            var result = customerService.AddCustomer(AName, AName, email, _aDateOfBirth, ACompanyId);

            _customerRepository.DidNotReceive().AddCustomer(_customer);
            Assert.IsFalse(result);
        }
    }
}