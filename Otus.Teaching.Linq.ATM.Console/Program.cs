using System;
using System.Linq;
using Otus.Teaching.Linq.Atm;
using Otus.Teaching.Linq.ATM.Core.Services;
using Otus.Teaching.Linq.ATM.DataAccess;

namespace Otus.Teaching.Linq.ATM.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Старт приложения-банкомата...");
            System.Console.WriteLine();

            var atmManager = CreateATMManager();
            var printer = new ConsolePrinter();

            var usr = atmManager.GetUser("snow", "111");
            printer.PrintUserInfo(usr);

            var userAccounts = atmManager.GetUserAccouts(usr.Id);
            printer.PrintUserAccounts(usr, userAccounts);

            var accountHistory = atmManager.GetUserAccountHistoryGrouped(usr.Id);
            printer.PrintUserAccountsHistory(usr, accountHistory);

            var incomeTransactions = atmManager.GetIncomeTransactions();
            printer.PrintIncomeTransactions(incomeTransactions);

            var limitForAccount = 10000;
            var accountSaldoInfo = atmManager.GetUsersWithSaldoGreater(limitForAccount);
            printer.PrintAccountsExceedLimit(limitForAccount, accountSaldoInfo);

            var limitForAllAcounts = 100000M;
            var usersSaldoInfo = atmManager.GetUsersWithSaldoOnAllAccountsGreater(limitForAllAcounts);
            printer.PrintRichUsers(limitForAllAcounts, usersSaldoInfo);

            System.Console.WriteLine("Завершение работы приложения-банкомата...");
        }

        static ATMManager CreateATMManager()
        {
            using var dataContext = new ATMDataContext();
            var users = dataContext.Users.ToList();
            var accounts = dataContext.Accounts.ToList();
            var history = dataContext.History.ToList();
                
            return new ATMManager(accounts, users, history);
        }
    }
}