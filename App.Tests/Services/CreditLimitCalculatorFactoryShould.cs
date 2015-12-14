using System;
using App.Services.CreditLimitAmount;
using App.Services.CustomerCreditLimit;
using NSubstitute;
using NUnit.Framework;

namespace App.Tests.Services
{
    [TestFixture]
    public class CreditLimitCalculatorFactoryShould
    {
        private ICreditLimitAmountService _creditLimitAmountService;
        private const string VeryImportantClientName = "VeryImportantClient";
        private const string ImportantClientName = "ImportantClient";
        private const string OtherClientName = "Other Client";
        private const string AFirstname = "firstname";
        private const string ASurname = "surname";
        private readonly DateTime _aDateOfBirth = DateTime.Today;
        private CreditLimitCalculatorFactory _creditLimitCalculatorFactory;

        [SetUp]
        public void Init()
        {
            _creditLimitAmountService = Substitute.For<ICreditLimitAmountService>();
            _creditLimitCalculatorFactory = new CreditLimitCalculatorFactory(_creditLimitAmountService);
        }

        [TestCase(VeryImportantClientName, typeof(VeryImportantClientCreditLimitCalculator))]
        [TestCase(ImportantClientName, typeof(ImportantClientCreditLimitCalculator))]
        [TestCase(OtherClientName, typeof(OtherClientCreditLimitCalculator))]
        public void create_client_credit_limit_calculator_based_on_client_name(string clientName, Type creditLimitCalculatorType)
        {
            var creditLimitCalculator = _creditLimitCalculatorFactory.GetCreditLimitCalculator(clientName, AFirstname, ASurname, _aDateOfBirth);

            Assert.That(creditLimitCalculator, Is.TypeOf(creditLimitCalculatorType));
        }

    }
}