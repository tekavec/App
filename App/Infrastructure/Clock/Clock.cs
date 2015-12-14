using System;

namespace App.Infrastructure.Clock
{
    public class Clock : IClock
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}