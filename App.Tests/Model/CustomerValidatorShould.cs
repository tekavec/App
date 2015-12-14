using System;
using App.Infrastructure;
using App.Model;
using NSubstitute;
using NUnit.Framework;

namespace App.Tests.Model
{
    [TestFixture]
    public class CustomerValidatorShould
    {
        private IClock _clock;

        private const string EmptyString = "";
        private const string AName = "AName";
        private const string AnEmail = "email@noemail.net";
        private const int HighCreditLimitAmount = 500;
        private const int LowCreditLimitAmount = 499;
        private const bool HasNoCreditLimit = false;
        private const bool HasCreditLimit = true;
        private readonly DateTime _today = new DateTime(2015, 1, 1);
        private readonly DateTime _dayOfBirthOfMinor = new DateTime(1994, 1, 2);
        private readonly DateTime _aDateOfBirthOfAdult = new DateTime(1994, 1, 1);
        private ICustomerValidator _customerValidator;

        [SetUp]
        public void Init()
        {
            _clock = Substitute.For<IClock>();
            _customerValidator = new CustomerValidator(_clock);
        }

        [TestCase(null, AName)]
        [TestCase(EmptyString, AName)]
        [TestCase(AName, null)]
        [TestCase(AName, EmptyString)]
        public void not_be_valid_if_firstname_or_surname_are_invalid(string firstname, string surname)
        {
            var result = _customerValidator.IsValid(firstname, surname, AnEmail, _aDateOfBirthOfAdult, HasCreditLimit, HighCreditLimitAmount);

            Assert.IsFalse(result);
        }

        [TestCase("incorrect.email")]
        [TestCase("incorrect@email")]
        [TestCase("incorrectemail")]
        public void not_be_valid_if_if_email_is_invalid(string email)
        {
            var result = _customerValidator.IsValid(AName, AName, email, _aDateOfBirthOfAdult, HasCreditLimit, HighCreditLimitAmount);

            Assert.IsFalse(result);
        }

        [Test]
        public void not_be_valid_if_age_not_legal()
        {
            _clock.Now().Returns(_today);
            var result = _customerValidator.IsValid(AName, AName, AnEmail, _dayOfBirthOfMinor, HasCreditLimit, HighCreditLimitAmount);

            Assert.IsFalse(result);
        }

        [Test]
        public void not_be_valid_if_credit_limit_not_sufficient()
        {
            var result = _customerValidator.IsValid(AName, AName, AnEmail, _aDateOfBirthOfAdult, HasCreditLimit, LowCreditLimitAmount);

            Assert.IsFalse(result);
        }

        [TestCase(HasCreditLimit, HighCreditLimitAmount)]
        [TestCase(HasNoCreditLimit, LowCreditLimitAmount)]
        public void be_valid_if_firstname_and_surname_and_email_are_valid_and_credit_limit_is_sufficient(bool hasCreditLimit, int creditLimitAmount)
        {
            _clock.Now().Returns(_today);
            var result = _customerValidator.IsValid(AName, AName, AnEmail, _aDateOfBirthOfAdult, hasCreditLimit, creditLimitAmount);

            Assert.IsTrue(result);
        }

    }
}