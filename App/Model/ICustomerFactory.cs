using System;

namespace App.Model
{
    public interface ICustomerFactory
    {
        Customer CreateCustomer(string firstname, string surname, string emailAddress, DateTime dateOfBirth, Company company, ICreditLimit creditLimit);
    }
}