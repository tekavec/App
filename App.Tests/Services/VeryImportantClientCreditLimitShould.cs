using App.Services;
using NUnit.Framework;

namespace App.Tests.Services
{
    [TestFixture]
    public class VeryImportantClientCreditLimitShould
    {

        [Test]
        public void create_an_unlimited_credit_limit_for_a_very_important_client()
        {
            var veryImportantClientCreditLimit = new VeryImportantClientCreditLimit();
            
            var creditLimit = veryImportantClientCreditLimit.GetCreditLimit();

            Assert.IsFalse(creditLimit.HasCreditLimit);
        }
    }
}