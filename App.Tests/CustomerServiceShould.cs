using System;
using App.Infrastructure.Company;
using App.Infrastructure.Customer;
using App.Infrastructure.Exceptions;
using App.Model;
using App.Services.CustomerCreditLimit;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace App.Tests
{
    [TestFixture]
    public class CustomerServiceShould
    {
        private ICustomerRepository _customerRepository;
        private ICustomerFactory _customerFactory;
        private ICompanyRepository _companyRepository;
        private ICreditLimitCalculatorFactory _creditLimitCalculatorFactory;
        private ICreditLimitCalculator _creditLimitCalculator;
        private ICreditLimit _aCreditLimit;
        private const string AFirstname = "firstname";
        private const string ASurname = "surname";
        private const string AnEmail = "email@noemail.net";
        private const int ACompanyId = 1;
        private const string ACompanyName = "company name";
        private readonly DateTime _aDateOfBirth = DateTime.Today;
        private Company _aCompany;

        [SetUp]
        public void Init()
        {
            _customerRepository = Substitute.For<ICustomerRepository>();
            _customerFactory = Substitute.For<ICustomerFactory>();
            _companyRepository = Substitute.For<ICompanyRepository>();
            _creditLimitCalculatorFactory = Substitute.For<ICreditLimitCalculatorFactory>();
            _creditLimitCalculator = Substitute.For<ICreditLimitCalculator>();
            _aCreditLimit = Substitute.For<ICreditLimit>();
            _aCompany = new Company {Id = ACompanyId, Name = ACompanyName};
            _companyRepository.GetById(ACompanyId).Returns(_aCompany);
            _creditLimitCalculatorFactory.GetCreditLimitCalculator(_aCompany.Name, AFirstname, ASurname, _aDateOfBirth)
                .Returns(_creditLimitCalculator);
            _creditLimitCalculator.GetCreditLimit().Returns(_aCreditLimit);
        }

        [Test]
        public void store_a_customer_and_return_a_positive_result_if_customer_can_be_created()
        {
            var customerService = new CustomerService(_customerRepository, _customerFactory, _companyRepository, _creditLimitCalculatorFactory);
            var customer = new Customer();
            _customerFactory.CreateCustomer(AFirstname, ASurname, AnEmail, _aDateOfBirth, _aCompany, _aCreditLimit)
                .Returns(customer);

            var result = customerService.AddCustomer(AFirstname, ASurname, AnEmail, _aDateOfBirth, ACompanyId);

            _customerRepository.Received().AddCustomer(customer);
            Assert.IsTrue(result);
        }

        [Test]
        public void not_store_a_customer_and_return_a_negative_result_if_customer_can_not_be_created()
        {
            var customerService = new CustomerService(_customerRepository, _customerFactory, _companyRepository, _creditLimitCalculatorFactory);
            _customerFactory.CreateCustomer(AFirstname, ASurname, AnEmail, _aDateOfBirth, _aCompany, _aCreditLimit)
                .Throws(new CreatingCustomerNotAllowedException());

            var result = customerService.AddCustomer(AFirstname, ASurname, AnEmail, _aDateOfBirth, ACompanyId);

            Assert.IsFalse(result);
        }

    }
}