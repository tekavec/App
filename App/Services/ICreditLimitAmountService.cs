using App.Model;

namespace App.Services
{
    public interface ICreditLimitAmountService
    {
        int GetCreditLimitAmount(ICustomer customer);
    }
}