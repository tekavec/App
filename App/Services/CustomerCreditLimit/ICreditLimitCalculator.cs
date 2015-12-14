using App.Model;

namespace App.Services.CustomerCreditLimit
{
    public interface ICreditLimitCalculator
    {
        CreditLimit GetCreditLimit();
    }
}