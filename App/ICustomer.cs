using System;

namespace App
{
    public interface ICustomer
    {
        int Id { get; set; }
        string Firstname { get; set; }
        string Surname { get; set; }
        DateTime DateOfBirth { get; set; }
        string EmailAddress { get; set; }
        bool HasCreditLimit { get; set; }
        int CreditLimit { get; set; }
        Company Company { get; set; }
    }
}