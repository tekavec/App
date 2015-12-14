using App.Model;

namespace App.Services
{
    public interface ICreditLimitService
    {
        void SetCreditLimitTo(ICustomer customer);
    }
}