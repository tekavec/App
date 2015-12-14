using App.Model;

namespace App.Services
{
    public interface ICreditLimitCalculator
    {
        CreditLimit GetCreditLimit();
    }
}