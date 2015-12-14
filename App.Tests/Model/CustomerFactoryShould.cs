using System;
using App.Infrastructure.Company;
using App.Infrastructure.Exceptions;
using App.Model;
using App.Services.CustomerCreditLimit;
using NSubstitute;
using NUnit.Framework;

namespace App.Tests.Model
{
    [TestFixture]
    public class CustomerFactoryShould
    {
        private ICompanyRepository _companyRepository;
        private ICreditLimitCalculatorFactory _creditLimitCalculatorFactory;
        private ICustomerValidator _customerValidator;
        private ICreditLimitCalculator _creditLimitCalculator;
        private const string AFirstname = "firstname";
        private const string ASurname = "surname";
        private const string ACompanyName = "company name";
        private const string AnEmail = "email@noemail.net";
        private readonly DateTime _aDateOfBirth = new DateTime(2015, 1, 1);
        private const int ACompanyId = 1;
        private readonly int _creditLimitAmount = new Random(1000).Next();
        private const bool HasNoCreditLimit = false;
        private CreditLimit _aCreditLimit;
        private ICustomerFactory _customerFactory;
        private Company _aCompany;

        [SetUp]
        public void Init()
        {
            _companyRepository = Substitute.For<ICompanyRepository>();
            _creditLimitCalculatorFactory = Substitute.For<ICreditLimitCalculatorFactory>();
            _customerValidator = Substitute.For<ICustomerValidator>();
            _creditLimitCalculator = Substitute.For<ICreditLimitCalculator>();
            _aCreditLimit = new CreditLimit(HasNoCreditLimit, _creditLimitAmount);
            _customerFactory = new CustomerFactory(_customerValidator);
            _aCompany = new Company { Id = ACompanyId, Name = ACompanyName };
            _companyRepository.GetById(ACompanyId).Returns(_aCompany);
            _creditLimitCalculatorFactory.GetCreditLimitCalculator(_aCompany.Name, AFirstname, ASurname, _aDateOfBirth)
                .Returns(_creditLimitCalculator);
            _creditLimitCalculator.GetCreditLimit().Returns(_aCreditLimit);
        }

        [Test]
        public void throw_an_exception_if_validation_not_passed()
        {
            _customerValidator.IsValid(AFirstname, ASurname, AnEmail, _aDateOfBirth, HasNoCreditLimit, _creditLimitAmount)
                .Returns(false);

            TestDelegate testDelegate = () => _customerFactory.CreateCustomer(AFirstname, ASurname, AnEmail, _aDateOfBirth, _aCompany, _aCreditLimit);

            Assert.Throws<CreatingCustomerNotAllowedException>(testDelegate);
        }

        [Test]
        public void create_a_new_customer_if_validation_passed()
        {
            _customerValidator.IsValid(AFirstname, ASurname, AnEmail, _aDateOfBirth, HasNoCreditLimit, _creditLimitAmount)
                .Returns(true);

            var customer = _customerFactory.CreateCustomer(AFirstname, ASurname, AnEmail, _aDateOfBirth, _aCompany, _aCreditLimit);

            Assert.That(customer, Is.TypeOf<Customer>());
        }

    }
}