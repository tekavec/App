using System;
using App.Infrastructure;
using App.Model;
using App.Services;
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
        private const int HighCreditLimitAmount = 500;
        private const int LowCreditLimitAmount = 499;
        private const bool HasNoCreditLimit = false;
        private const bool HasCreditLimit = true;
        private readonly DateTime _today = new DateTime(2015, 1, 1);
        private readonly DateTime _dayOfBirthOfMinor = new DateTime(1994, 1, 2);
        private readonly DateTime _aDateOfBirthOfAdult = new DateTime(1994, 1, 1);

        [SetUp]
        public void Init()
        {
            _customer = Substitute.For<ICustomer>();
            _clock = Substitute.For<IClock>();
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

            var result = customerService.AddCustomer(firstname, surname, AnEmail, _aDateOfBirthOfAdult, ACompanyId);

            _customerRepository.DidNotReceive().AddCustomer(_customer);
            Assert.IsFalse(result);
        }

        [TestCase("incorrect.email")]
        [TestCase("incorrect@email")]
        [TestCase("incorrectemail")]
        public void not_store_a_customer_if_email_is_invalid_and_report_negative_result(string email)
        {
            var customerService = new CustomerService(_customer, _clock, _companyRepository, _creditLimitService, _customerRepository);

            var result = customerService.AddCustomer(AName, AName, email, _aDateOfBirthOfAdult, ACompanyId);

            _customerRepository.DidNotReceive().AddCustomer(_customer);
            Assert.IsFalse(result);
        }

        [Test]
        public void not_store_a_customer_if_age_not_legal_and_report_negative_result()
        {
            var customerService = new CustomerService(_customer, _clock, _companyRepository, _creditLimitService, _customerRepository);
            _clock.Now().Returns(_today);

            var result = customerService.AddCustomer(AName, AName, AnEmail, _dayOfBirthOfMinor, ACompanyId);

            _customerRepository.DidNotReceive().AddCustomer(_customer);
            Assert.IsFalse(result);
        }

        [Test]
        public void not_store_a_customer_if_credit_limit_not_sufficient_and_report_negative_result()
        {
            var customerService = new CustomerService(_customer, _clock, _companyRepository, _creditLimitService, _customerRepository);
            _clock.Now().Returns(_today);
            _customer.HasCreditLimit.Returns(HasCreditLimit);
            _customer.CreditLimit.Returns(LowCreditLimitAmount);

            var result = customerService.AddCustomer(AName, AName, AnEmail, _aDateOfBirthOfAdult, ACompanyId);

            _customerRepository.DidNotReceive().AddCustomer(_customer);
            Assert.IsFalse(result);
        }

        [TestCase(HasCreditLimit, HighCreditLimitAmount)]
        [TestCase(HasNoCreditLimit, LowCreditLimitAmount)]
        public void store_a_valid_customer_and_report_positive_result(bool hasCreditLimit, int creditLimitAmount)
        {
            var customerService = new CustomerService(_customer, _clock, _companyRepository, _creditLimitService, _customerRepository);
            _clock.Now().Returns(_today);
            _customer.HasCreditLimit.Returns(hasCreditLimit);
            _customer.CreditLimit.Returns(creditLimitAmount);

            var result = customerService.AddCustomer(AName, AName, AnEmail, _aDateOfBirthOfAdult, ACompanyId);

            _customerRepository.Received().AddCustomer(_customer);
            Assert.IsTrue(result);
        }
    }
}