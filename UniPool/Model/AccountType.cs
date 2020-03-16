using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniPool.Model
{
    public class AccountType : SmartEnum<AccountType>
    {
        public static readonly AccountType Driver = new AccountType(nameof(Driver), 1);
        public static readonly AccountType Passenger = new AccountType(nameof(Passenger), 2);

        public AccountType(string name, int value) : base(name, value)
        {
        }
    }
}
