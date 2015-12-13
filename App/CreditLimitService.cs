namespace App
{
    public class CreditLimitService : ICreditLimitService
    {
        public void SetCreditLimitTo(ICustomer customer)
        {
            if (customer.Company.Name == "VeryImportantClient")
            {
                // Skip credit check
                customer.HasCreditLimit = false;
            }
            else if (customer.Company.Name == "ImportantClient")
            {
                // Do credit check and double credit limit
                customer.HasCreditLimit = true;
                using (var customerCreditService = new CustomerCreditServiceClient())
                {
                    var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname,
                        customer.DateOfBirth);
                    creditLimit = creditLimit*2;
                    customer.CreditLimit = creditLimit;
                }
            }
            else
            {
                // Do credit check
                customer.HasCreditLimit = true;
                using (var customerCreditService = new CustomerCreditServiceClient())
                {
                    var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname,
                        customer.DateOfBirth);
                    customer.CreditLimit = creditLimit;
                }
            }
        }
    }
}