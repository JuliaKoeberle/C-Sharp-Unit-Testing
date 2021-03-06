﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Unit
{
    /// <summary>
    /// NUnit tests with Moq
    /// </summary>
    public interface IAuthorize
    {
        bool Authorized();
    }
    class BankAccountWithMoq
    {
        private ILog log;
        private IAuthorize authorize;
        public int Balance { get; set; }

        public BankAccountWithMoq(ILog log, IAuthorize authorize)
        {
            this.log = log;
            this.authorize = authorize;
        }

        public void Deposit(int amount)
        {
            if(!authorize.Authorized()) throw new AccessViolationException("You are not logged in!");
            log.Write(@"Deposited {amount}.");
            Balance += amount;
        }
    }
    [TestFixture]
    public class BankAcountWithMoqTests
    {
        private BankAccountWithMoq ba;

        /// <summary>
        /// Test authorized with moq object injected returning true
        /// </summary>
        [Test]
        public void DepositAuthorizedTest()
        {
            var log = new Mock<ILog>();
            var auth = new Mock<IAuthorize>();
            auth.Setup(a => a.Authorized()).Returns(true);

            ba = new BankAccountWithMoq(log.Object, auth.Object) { Balance = 100 };

            ba.Deposit(100);
            Assert.That(ba.Balance, Is.EqualTo(200));
        }

        /// <summary>
        /// Test injected autorize with moq object returning false
        /// </summary>
        [Test]
        public void DisallowDepositIfNotLoggedInTest()
        {
            var log = new Mock<ILog>();
            var auth = new Mock<IAuthorize>();
            auth.Setup(a => a.Authorized()).Returns(false);

            ba = new BankAccountWithMoq(log.Object, auth.Object) { Balance = 100 };
            Assert.Multiple(() =>
            {
                Assert.That(() => ba.Deposit(100), Throws.TypeOf<AccessViolationException>());
                Assert.That(ba.Balance, Is.EqualTo(100));
            });         
        }
    }
}
