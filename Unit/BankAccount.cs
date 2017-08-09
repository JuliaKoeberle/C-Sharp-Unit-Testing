using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Unit
{
    public class BankAccount
    {
        public int Balance { get; private set; }

        public BankAccount(int startingBalance)
        {
            Balance = startingBalance;
        }

        public void Deposit(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Invalid Deposit of $" + amount);
            }
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }

        public bool WithdrawWithVerification(int amount)
        {
            if(Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }

    [TestFixture]
    public class BankAccountTests
    {
        private BankAccount ba;
        [SetUp]
        public void SetUp()
        {
            ba = new BankAccount(100);
        }

        [Test]
        public void BalanceShouldIncreaseOnPositiveDeposit()
        {
            //arrange


            //act
            ba.Deposit(100);

            //assert
            Assert.That(ba.Balance, Is.EqualTo(200));
        }

        [Test]
        public void Multiple()
        {
            ba.Withdraw(100);

            Assert.Multiple(() =>
            {
                Assert.That(ba.Balance, Is.EqualTo(0));
                Assert.That(ba.Balance, Is.LessThan(1));
            });
        }

        [Test]
        public void BankAccountShouldThrowOnNegativeAmlunt()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => ba.Deposit(-1)
            );
            StringAssert.StartsWith("Invalid Deposit", ex.Message);
        }
    }

    [TestFixture]
    public class DataDrivenTests
    {
        private BankAccount ba;

        [SetUp]
        public void SetUp()
        {
            ba = new BankAccount(100);
        }

        [Test]
        [TestCase(50, true, 50)]
        [TestCase(100, true, 0)]
        [TestCase(1000, false, 100)]
        public void TestMultipleWithdraws(int amount, bool shouldSucced, int expectedBalance)
        {
            var result = ba.WithdrawWithVerification(amount);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(shouldSucced));
                Assert.That(expectedBalance, Is.EqualTo(ba.Balance));
            });

        }
    }
}
