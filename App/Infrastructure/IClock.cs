using System;

namespace App.Infrastructure
{
    public interface IClock
    {
        DateTime Now();
    }
}