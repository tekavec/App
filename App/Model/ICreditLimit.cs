namespace App.Model
{
    public interface ICreditLimit
    {
        bool HasCreditLimit { get; }
        int CreditLimitAmount { get; }
    }
}