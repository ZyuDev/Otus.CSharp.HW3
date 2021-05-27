using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Otus.Teaching.Linq.ATM.DataAccess;
using Otus.Teaching.Linq.ATM.Core;
using Otus.Teaching.Linq.ATM.Core.Services;
using System.Linq;

namespace Otus.ATM.UnitTests
{

    [TestFixture]
    public class ATMManagerTests
    {
        private ATMManager _manager;


        [SetUp]
        public void SetUp()
        {
            using var dataContext = new ATMDataContext();
            var users = dataContext.Users.ToList();
            var accounts = dataContext.Accounts.ToList();
            var history = dataContext.History.ToList();

            _manager = new ATMManager(accounts, users, history);

        }

        [Test]
        public void GetUser_WrongLoginPassword_ReturnNull()
        {
            var usr = _manager.GetUser("wrong_login", "wrong_pwd");

            Assert.IsNull(usr);
        }

        [Test]
        public void GetUser_EmptyLoginPassword_ReturnNull()
        {
            var usr = _manager.GetUser(null, "");

            Assert.IsNull(usr);
        }

        [Test]
        public void GetUser_CorrectLoginPassword_ReturnUser()
        {
            var usr = _manager.GetUser("lee", "222");

            Assert.AreEqual(2, usr.Id);
        }

        [Test]
        public void GetUser_LoginWrongCase_ReturnUser()
        {
            var usr = _manager.GetUser("lEe", "222");

            Assert.AreEqual(2, usr.Id);
        }

        [Test]
        public void GetUsersWithSaldoGreater_Zero_AllAccounts()
        {
            var results = _manager.GetUsersWithSaldoGreater(0);

            Assert.AreEqual(9, results.Count());
        }

        [Test]
        public void GetUsersWithSaldoGreater_BigNumber_ZeroAccounts()
        {
            var results = _manager.GetUsersWithSaldoGreater(10000000);

            Assert.AreEqual(0, results.Count());
        }

        [Test]
        public void GetUsersWithSaldoGreater_TenThousands_ZeroAccounts()
        {
            var results = _manager.GetUsersWithSaldoGreater(10000);

            Assert.AreEqual(3, results.Count());
        }

        [Test]
        public void GetUsersWithSaldoGreater_CheckBoderCondition_ZeroAccounts()
        {
            var results = _manager.GetUsersWithSaldoGreater(9999);

            Assert.AreEqual(5, results.Count());
        }

        [Test]
        public void GetIncomeTransactions_ReturnAllIncomeTransactions()
        {
            var results = _manager.GetIncomeTransactions();

            Assert.AreEqual(10, results.Count());
        }

        [Test]
        public void GetUserAccouts_User1_ReturnThreeAccounts()
        {
            var result = _manager.GetUserAccouts(1);

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void GetUserAccouts_User2_ReturnTwoAccounts()
        {
            var result = _manager.GetUserAccouts(1);

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void GetUserAccouts_UserUnknown_ReturnZeroAccounts()
        {
            var result = _manager.GetUserAccouts(-1);

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void GetUserAccountHistory_User1_ReturnSixRecords()
        {
            var result = _manager.GetUserAccountHistory(1);

            Assert.AreEqual(6, result.Count());
        }

        [Test]
        public void GetUserAccountHistory_UserUnknown_ReturnZeroRecords()
        {
            var result = _manager.GetUserAccountHistory(-1);

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void GetUsersWithSaldoOnAllAccountsGreater_OneHundredThousands_ReturnRecords()
        {
            var result = _manager.GetUsersWithSaldoOnAllAccountsGreater(100000);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(101100M, result[0].CashAll);
        }
    }
}
