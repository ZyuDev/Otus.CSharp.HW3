using System.Collections.Generic;
using Otus.Teaching.Linq.ATM.Core.Entities;
using System.Linq;

namespace Otus.Teaching.Linq.ATM.Core.Services
{
    public class ATMManager
    {
        public IEnumerable<Account> Accounts { get; private set; }
        
        public IEnumerable<User> Users { get; private set; }
        
        public IEnumerable<OperationsHistory> History { get; private set; }
        
        public ATMManager(IEnumerable<Account> accounts, IEnumerable<User> users, IEnumerable<OperationsHistory> history)
        {
            Accounts = accounts;
            Users = users;
            History = history;
        }

        public User GetUser(string login, string pwd)
        {
            return Users.FirstOrDefault(x => x.Login.Equals(login, System.StringComparison.OrdinalIgnoreCase) && x.Password.Equals(pwd));
        }

        public List<Account> GetUserAccouts(int userId)
        {
            return Accounts.Where(x => x.UserId == userId).ToList();
        }

        public List<OperationsHistory> GetUserAccountHistory(int userId)
        {
            var query = from account in Accounts 
                        where account.UserId == userId 
                        join transaction in History on account.Id equals transaction.AccountId 
                        select transaction;

            return query.ToList();
        }

        public List<IGrouping<int, OperationsHistory>> GetUserAccountHistoryGrouped(int userId)
        {
            var query = from account in Accounts
                        where account.UserId == userId
                        join transaction in History on account.Id equals transaction.AccountId
                        group transaction by transaction.AccountId into g
                        select g;

            var result = query.ToList();

            return result;
        }

        public List<IncomeTransactionsResult> GetIncomeTransactions()
        {
            var query = from transaction in History
                        where transaction.OperationType == OperationType.InputCash
                        join account in Accounts on transaction.AccountId equals account.Id into at
                        from a in at
                        join user in Users on a.UserId equals user.Id into info
                        from u in info
                        select new IncomeTransactionsResult{ UserId = u.Id, 
                            FirstName = u.FirstName, 
                            SurName = u.SurName, 
                            AccountId = transaction.AccountId, 
                            TransactionId = transaction.Id, 
                            OperationDate = transaction.OperationDate, 
                            CashAll = transaction.CashSum };

            return query.ToList();
        }

        public List<UserSaldoResult> GetUsersWithSaldoGreater(decimal limit)
        {
            var query = from account in Accounts
                        where account.CashAll > limit
                        join user in Users on account.UserId equals user.Id into au
                        from u in au
                        orderby u.FirstName
                        select new UserSaldoResult{ UserId = u.Id, 
                            FirstName = u.FirstName, 
                            SurName = u.SurName, 
                            AccountId = account.Id, 
                            CashAll = account.CashAll };


            return query.ToList(); 
        }

        public List<UserSaldoResult> GetUsersWithSaldoOnAllAccountsGreater(decimal limit)
        {
            var query = from account in Accounts
                        group account by account.UserId into g
                        select new { UserId = g.Key, SumTotal = g.Sum(x => x.CashAll) } into totals
                        where totals.SumTotal > limit 
                        join user in Users on totals.UserId equals user.Id into au 
                        from u in au
                        orderby u.FirstName
                        select new UserSaldoResult
                        {
                            UserId = u.Id,
                            FirstName = u.FirstName,
                            SurName = u.SurName,
                            CashAll = totals.SumTotal
                        };


            return query.ToList();
        }
    }
}