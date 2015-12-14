using App.Model;

namespace App.Services.CustomerCreditLimit
{
    public class VeryImportantClientCreditLimit : ICreditLimitCalculator
    {
        public bool HasNoCreditLimit = false;

        public ICreditLimit GetCreditLimit()
        {
            return new CreditLimit(HasNoCreditLimit, new int());
        }

    }
}