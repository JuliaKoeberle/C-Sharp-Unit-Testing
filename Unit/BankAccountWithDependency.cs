using System;
using NUnit.Framework;

/// <summary>
/// Static Fakes demo
/// </summary>
namespace Unit
{
    public interface ILog
    {
        void Write(string msg);
    }

    public class ConsoleLog : ILog
    {
        //this is a real concrete class
        public void Write(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public class NullLog : ILog
    {
        //this is a fake class for testing
        public void Write(string msg)
        {
            //nothing happens here; an empty call with no side effects
        }
    }

    public class BankAccountWithDependency
    {
        public int Balance { get; set; }
        private readonly ILog log;

        public BankAccountWithDependency(ILog log)
        {
            this.log = log;
            Balance = 100;
        }

        public void Deposit(int amount)
        {
            Balance += amount;
            log.Write(@"Deposited {amount}");
        }
    }


    [TestFixture]
    public class BankAccountWithDependencyTests
    {
        private BankAccountWithDependency ba;

        [Test]
        public void DepositIntegrationTest()
        {
            var fakeLog = new NullLog();
            ba = new BankAccountWithDependency(fakeLog);
            ba.Deposit(100);
            Assert.That(ba.Balance, Is.EqualTo(200));
        }
    }
}
