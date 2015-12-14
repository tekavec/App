using App.Model;
using NUnit.Framework;

namespace App.Tests.Model
{
    [TestFixture]
    public class CreditLimitShould
    {
        private const bool HasCreditLimit = true;
        private const bool HasNoCreditLimit = false;
        private const int CreditLimitAmountOne = 1000;
        private const int CreditLimitAmountTwo = 2000;

        [Test]
        public void be_equal_if_all_properties_are_equal()
        {
            var creditLimitOne = new CreditLimit(HasCreditLimit, CreditLimitAmountOne);
            var creditLimitTwo = new CreditLimit(HasCreditLimit, CreditLimitAmountOne);

            Assert.AreEqual(creditLimitOne, creditLimitTwo);
        }

        [TestCase(HasCreditLimit, CreditLimitAmountOne, HasCreditLimit, CreditLimitAmountTwo)]
        [TestCase(HasCreditLimit, CreditLimitAmountOne, HasNoCreditLimit, CreditLimitAmountOne)]
        [TestCase(HasCreditLimit, CreditLimitAmountOne, HasNoCreditLimit, CreditLimitAmountTwo)]
        public void not_be_equal_if_properties_are_not_equal(bool hasCreditLimitOne, int creditLimitAmountOne, bool hasCreditLimitTwo, int creditLimitAmountTwo)
        {
            var creditLimitOne = new CreditLimit(hasCreditLimitOne, creditLimitAmountOne);
            var creditLimitTwo = new CreditLimit(hasCreditLimitTwo, creditLimitAmountTwo);

            Assert.AreNotEqual(creditLimitOne, creditLimitTwo);
        }

    }
}