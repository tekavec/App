namespace App.Model
{
    public class CreditLimit
    {
        private readonly bool _hasCreditLimit;
        private readonly int _creditLimitAmount;

        public CreditLimit(bool hasCreditLimit, int creditLimitAmount)
        {
            _hasCreditLimit = hasCreditLimit;
            _creditLimitAmount = creditLimitAmount;
        }

        public bool HasCreditLimit => _hasCreditLimit;

        public int CreditLimitAmount => _creditLimitAmount;

        protected bool Equals(CreditLimit other)
        {
            return _hasCreditLimit == other._hasCreditLimit && _creditLimitAmount == other._creditLimitAmount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CreditLimit) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_hasCreditLimit.GetHashCode()*397) ^ _creditLimitAmount;
            }
        }
    }
}