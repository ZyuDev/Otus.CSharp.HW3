using Otus.Teaching.Linq.ATM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Otus.Teaching.Linq.Atm
{
    public class ConsolePrinter
    {
        public void PrintUserInfo(User user)
        {
            if (user == null)
            {
                Console.WriteLine("User not found!");
                return;
            }

            Console.WriteLine($"User \"{user}\" info:");
            PrintDelimiter();

            var props = user.GetType().GetProperties();

            foreach (var prop in props)
            {
                Console.WriteLine($"{prop.Name}: {prop.GetValue(user)}");
            }

            PrintDelimiter();
            Console.WriteLine();
        }

        public void PrintUserAccounts(User usr, IEnumerable<Account> accounts)
        {
            Console.WriteLine($"\"{usr}\" has following accounts:");
            PrintDelimiter();

            var n = 1;
            foreach (var account in accounts)
            {
                Console.WriteLine($"{n}. {account}");
                n++;
            }

            PrintDelimiter();
            Console.WriteLine();

        }

        public void PrintUserAccountsHistory(User usr, List<IGrouping<int, OperationsHistory>> results)
        {
            Console.WriteLine($"Operations history for \"{usr}\":");
            PrintDelimiter();

            foreach (var g in results)
            {
                Console.WriteLine($"Account: {g.Key}");
                foreach (var item in g)
                {
                    Console.WriteLine(item);
                }
            }

            PrintDelimiter();
            Console.WriteLine();
        }

        public void PrintIncomeTransactions(IEnumerable<IncomeTransactionsResult> transactions)
        {
            Console.WriteLine($"Income transactions:");
            PrintDelimiter();

            var n = 1;
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{n}. {transaction}");
                n++;
            }

            PrintDelimiter();
            Console.WriteLine();

        }

        public void PrintAccountsExceedLimit(decimal limit, IEnumerable<UserSaldoResult> results)
        {
            Console.WriteLine($"Accounts exceed limit {limit}:");
            PrintDelimiter();

            var n = 1;
            foreach (var item in results)
            {
                Console.WriteLine($"{n}. {item}");
                n++;
            }

            PrintDelimiter();
            Console.WriteLine();
        }

        public void PrintRichUsers(decimal limit, IEnumerable<UserSaldoResult> results)
        {
            Console.WriteLine($"Users exceed limit {limit} on all accounts:");
            PrintDelimiter();

            var n = 1;
            foreach(var item in results)
            {
                Console.WriteLine($"{n}. User \"{item.FirstName} {item.SurName}\" has on all accounts: {item.CashAll}");
                n++;
            }

            PrintDelimiter();
            Console.WriteLine();
        }

        public void PrintDelimiter()
        {
            Console.WriteLine("==================");
        }
    }
}
