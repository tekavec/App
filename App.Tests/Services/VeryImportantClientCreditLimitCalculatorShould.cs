using App.Services.CustomerCreditLimit;
using NUnit.Framework;

namespace App.Tests.Services
{
    [TestFixture]
    public class VeryImportantClientCreditLimitCalculatorShould
    {

        [Test]
        public void create_an_unlimited_credit_limit_for_a_very_important_client()
        {
            var veryImportantClientCreditLimit = new VeryImportantClientCreditLimitCalculator();
            
            var creditLimit = veryImportantClientCreditLimit.GetCreditLimit();

            Assert.IsFalse(creditLimit.HasCreditLimit);
        }
    }
}