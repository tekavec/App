using App.Model;

namespace App.Services.CustomerCreditLimit
{
    public class VeryImportantClientCreditLimit : ICreditLimitCalculator
    {
        public bool HasNoCreditLimit = false;

        public CreditLimit GetCreditLimit()
        {
            return new CreditLimit(HasNoCreditLimit, new int());
        }

    }
}