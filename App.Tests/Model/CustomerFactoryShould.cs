using System;
using App.Infrastructure;
using App.Model;
using App.Services;
using NSubstitute;
using NUnit.Framework;

namespace App.Tests.Model
{
    [TestFixture]
    public class CustomerFactoryShould
    {
        private ICompanyRepository _companyRepository;
        private ICreditLimitService _creditLimitService;
        private ICustomerValidator _customerValidator;
        private const string AName = "AName";
        private const string AnEmail = "email@noemail.net";
        private readonly DateTime _aDateOfBirth = new DateTime(2015, 1, 1);
        private const int ACompanyId = 1;
        private readonly int _creditLimitAmount = new Random(1000).Next();
        private const bool HasNoCreditLimit = false;


        [SetUp]
        public void Init()
        {
            _companyRepository = Substitute.For<ICompanyRepository>();
            _creditLimitService = Substitute.For<ICreditLimitService>();
            _customerValidator = Substitute.For<ICustomerValidator>();
        }

        [Test]
        public void throw_a_creating_customer_not_allowed_exception_if_validation_not_passed()
        {
            var customerFactory = new CustomerFactory(_companyRepository, _creditLimitService, _customerValidator);
            var company = new Company { Id = ACompanyId, Name = AName };
            _companyRepository.GetById(ACompanyId).Returns(company);
            _creditLimitService.GetCreditLimit(company.Name, AName, AName, _aDateOfBirth).Returns(new CreditLimit(HasNoCreditLimit, _creditLimitAmount));
            _customerValidator.IsValid(AName, AName, AnEmail, _aDateOfBirth, HasNoCreditLimit, _creditLimitAmount).Returns(false);

            TestDelegate testDelegate = () => customerFactory.CreateCustomer(AName, AName, AnEmail, _aDateOfBirth, ACompanyId);

            Assert.Throws<CreatingCustomerNotAllowedException>(testDelegate);
        }

        [Test]
        public void create_a_new_customer_if_validation_passed()
        {
            var customerFactory = new CustomerFactory(_companyRepository, _creditLimitService, _customerValidator);
            var company = new Company { Id = ACompanyId, Name = AName };
            _companyRepository.GetById(ACompanyId).Returns(company);
            _creditLimitService.GetCreditLimit(company.Name, AName, AName, _aDateOfBirth).Returns(new CreditLimit(HasNoCreditLimit, _creditLimitAmount));
            _customerValidator.IsValid(AName, AName, AnEmail, _aDateOfBirth, HasNoCreditLimit, _creditLimitAmount).Returns(true);

            var customer = customerFactory.CreateCustomer(AName, AName, AnEmail, _aDateOfBirth, ACompanyId);

            Assert.That(customer, Is.TypeOf<Customer>());
        }

    }
}