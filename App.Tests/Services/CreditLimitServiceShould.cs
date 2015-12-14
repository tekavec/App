using System;
using App.Model;
using App.Services;
using NSubstitute;
using NUnit.Framework;

namespace App.Tests.Services
{
    [TestFixture]
    public class CreditLimitServiceShould
    {
        private ICreditLimitAmountService _creditLimitAmountService;
        private const string VeryImportantClientName = "VeryImportantClient";
        private const string ImportantClientName = "ImportantClient";
        private const string OtherClientName = "Other Client";
        private const string AString = "SomeString";
        private readonly DateTime _aDateOfBirth = DateTime.Today;
        private bool _HasCreditLimit = true;
        private bool _HasNoCreditLimit = false;
        private CreditLimitService _creditLimitService;

        [SetUp]
        public void Init()
        {
            _creditLimitAmountService = Substitute.For<ICreditLimitAmountService>();
            _creditLimitService = new CreditLimitService(_creditLimitAmountService);
        }

        [Test]
        public void set_unlimited_credit_limit_to_customer_for_very_important_client()
        {
            var creditLimit = _creditLimitService.SetCreditLimitTo(VeryImportantClientName, AString, AString, _aDateOfBirth);

            Assert.IsFalse(creditLimit.HasCreditLimit);
        }

        [Test]
        public void set_credit_limit_to_customer_for_important_client()
        {
            var creditLimit = _creditLimitService.SetCreditLimitTo(ImportantClientName, AString, AString, _aDateOfBirth);

            Assert.IsTrue(creditLimit.HasCreditLimit);
        }

        [Test]
        public void double_credit_limit_amount_to_customer_for_important_client()
        {
            int aCreditLimitAmount = new Random(1000).Next();
            _creditLimitAmountService.GetCreditLimitAmount(AString, AString, _aDateOfBirth).Returns(aCreditLimitAmount);

            var creditLimit = _creditLimitService.SetCreditLimitTo(ImportantClientName, AString, AString, _aDateOfBirth);

            Assert.That(creditLimit.CreditLimitAmount, Is.EqualTo(aCreditLimitAmount * 2));
        }

        [Test]
        public void set_credit_limit_to_customer_for_other_client()
        {
            var creditLimit = _creditLimitService.SetCreditLimitTo(OtherClientName, AString, AString, _aDateOfBirth);

            Assert.IsTrue(creditLimit.HasCreditLimit);
        }

        [Test]
        public void set_credit_limit_amount_to_customer_for_other_client()
        {
            int aCreditLimitAmount = new Random(1000).Next();
            _creditLimitAmountService.GetCreditLimitAmount(AString, AString, _aDateOfBirth).Returns(aCreditLimitAmount);

            var creditLimit = _creditLimitService.SetCreditLimitTo(OtherClientName, AString, AString, _aDateOfBirth);

            Assert.That(creditLimit.CreditLimitAmount, Is.EqualTo(aCreditLimitAmount));
        }

    }
}