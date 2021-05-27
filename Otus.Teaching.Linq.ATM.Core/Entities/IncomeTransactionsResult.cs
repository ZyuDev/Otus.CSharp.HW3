using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Linq.ATM.Core.Entities
{
    public class IncomeTransactionsResult
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public decimal CashAll { get; set; }
        public DateTime OperationDate { get; set; }

        public override string ToString()
        {
            return $"{OperationDate} +{CashAll} to account: {AccountId} of user \"{FirstName} {SurName}\", transaction: {TransactionId}";
        }

    }
}
