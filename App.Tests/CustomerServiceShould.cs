using System;
using App.Infrastructure.Customer;
using App.Infrastructure.Exceptions;
using App.Model;
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
        private const string AName = "AName";
        private const string AnEmail = "email@noemail.net";
        private const int ACompanyId = 1;
        private readonly DateTime _aDateOfBirthOfAdult = new DateTime(1994, 1, 1);

        [SetUp]
        public void Init()
        {
            _customerRepository = Substitute.For<ICustomerRepository>();
            _customerFactory = Substitute.For<ICustomerFactory>();
        }

        [Test]
        public void store_a_customer_if_customer_can_be_created()
        {
            var customerService = new CustomerService(_customerRepository, _customerFactory);
            var customer = new Customer();
            _customerFactory.CreateCustomer(AName, AName, AnEmail, _aDateOfBirthOfAdult, ACompanyId)
                .Returns(customer);

            var result = customerService.AddCustomer(AName, AName, AnEmail, _aDateOfBirthOfAdult, ACompanyId);

            Assert.IsTrue(result);
        }

        [Test]
        public void not_store_a_customer_if_customer_can_not_be_created()
        {
            var customerService = new CustomerService(_customerRepository, _customerFactory);
            _customerFactory.CreateCustomer(AName, AName, AnEmail, _aDateOfBirthOfAdult, ACompanyId)
                .Throws(new CreatingCustomerNotAllowedException());

            var result = customerService.AddCustomer(AName, AName, AnEmail, _aDateOfBirthOfAdult, ACompanyId);

            Assert.IsFalse(result);
        }

    }
}