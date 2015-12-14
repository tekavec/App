using System;
using App.Model;
using App.Services.CreditLimitAmount;
using App.Services.CustomerCreditLimit;
using NSubstitute;
using NUnit.Framework;

namespace App.Tests.Services
{
    [TestFixture]
    public class OtherClientCreditLimitCalculatorShould
    {
        private ICreditLimitAmountService _creditLimitAmountService;
        private const string AFirstname = "firstname";
        private const string ASurname = "surname";
        private const bool HasCreditLimit = true;
        private readonly DateTime _aDateOfBirth = DateTime.Today;
        private readonly int _aCreditLimitAmount = new Random(1000).Next();

        [SetUp]
        public void Init()
        {
            _creditLimitAmountService = Substitute.For<ICreditLimitAmountService>();
        }

        [Test]
        public void create_a_credit_limit_for_other_client()
        {
            var expectedCreditLimit = new CreditLimit(HasCreditLimit, _aCreditLimitAmount);
            var otherClientCreditLimit = new OtherClientCreditLimitCalculator(_creditLimitAmountService, AFirstname, ASurname, _aDateOfBirth);
            _creditLimitAmountService.GetCreditLimitAmount(AFirstname, ASurname, _aDateOfBirth)
                .Returns(_aCreditLimitAmount);

            var creditLimit = otherClientCreditLimit.GetCreditLimit();

            Assert.That(creditLimit, Is.EqualTo(expectedCreditLimit));
        }
    }
}