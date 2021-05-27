using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Linq.ATM.Core.Entities
{
    public class UserSaldoResult
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public int AccountId { get; set; }
        public decimal CashAll { get; set; }

        public override string ToString()
        {
            return $"\"{FirstName} {SurName}\" has on Account {AccountId}: {CashAll}";
        }
    }
}
