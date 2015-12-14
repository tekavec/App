using NUnit.Framework;

namespace App.Tests
{
    [TestFixture]
    public class CreditLimitServiceShould
    {
        private string _VeryImportantClientName = "VeryImportantClient";
        private bool _HasCreditLimit = true;
        private bool _HasNoCreditLimit = false;

        [Test]
        public void set_unlimited_credit_limit_to_customer_for_very_important_client()
        {
            var customer = new Customer {HasCreditLimit = _HasCreditLimit, Company = new Company {Name = _VeryImportantClientName } };
            var creditLimitService = new CreditLimitService();
            
            creditLimitService.SetCreditLimitTo(customer);

            Assert.IsFalse(customer.HasCreditLimit);
            
        }

    }
}