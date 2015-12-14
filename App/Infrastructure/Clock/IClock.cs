using System;

namespace App.Infrastructure.Clock
{
    public interface IClock
    {
        DateTime Now();
    }
}