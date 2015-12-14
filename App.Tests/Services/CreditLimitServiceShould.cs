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
            var customer = new Customer { HasCreditLimit = _HasCreditLimit, Company = new Company { Name = VeryImportantClientName } };

            _creditLimitService.SetCreditLimitTo(customer);

            Assert.IsFalse(customer.HasCreditLimit);
        }

        [Test]
        public void set_credit_limit_to_customer_for_important_client()
        {
            var customer = new Customer { HasCreditLimit = _HasNoCreditLimit, Company = new Company { Name = ImportantClientName } };

            _creditLimitService.SetCreditLimitTo(customer);

            Assert.IsTrue(customer.HasCreditLimit);
        }

        [Test]
        public void double_credit_limit_amount_to_customer_for_important_client()
        {
            var customer = new Customer { HasCreditLimit = _HasNoCreditLimit, Company = new Company { Name = ImportantClientName } };
            int aCreditLimitAmount = new Random(1000).Next();
            _creditLimitAmountService.GetCreditLimitAmount(customer).Returns(aCreditLimitAmount);

            _creditLimitService.SetCreditLimitTo(customer);

            Assert.That(customer.CreditLimit, Is.EqualTo(aCreditLimitAmount * 2));
        }

        [Test]
        public void set_credit_limit_to_customer_for_other_client()
        {
            var customer = new Customer { HasCreditLimit = _HasNoCreditLimit, Company = new Company { Name = OtherClientName } };

            _creditLimitService.SetCreditLimitTo(customer);

            Assert.IsTrue(customer.HasCreditLimit);
        }

        [Test]
        public void set_credit_limit_amount_to_customer_for_other_client()
        {
            var customer = new Customer { HasCreditLimit = _HasNoCreditLimit, Company = new Company { Name = OtherClientName } };
            int aCreditLimitAmount = new Random(1000).Next();
            _creditLimitAmountService.GetCreditLimitAmount(customer).Returns(aCreditLimitAmount);

            _creditLimitService.SetCreditLimitTo(customer);

            Assert.That(customer.CreditLimit, Is.EqualTo(aCreditLimitAmount));
        }

    }
}