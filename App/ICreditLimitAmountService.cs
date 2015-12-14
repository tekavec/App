using System;

namespace App
{
    public interface ICreditLimitAmountService
    {
        int GetCreditLimitAmount(ICustomer customer);
    }
}