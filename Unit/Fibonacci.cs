using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Unit
{
    /// <summary>
    /// Calculate Fib number linearly and recursively
    /// </summary>
    public class Fibonacci
    {
        /// <summary>
        /// Recursively calculate n-th Fib number
        /// </summary>
        /// <param name="num"></param>
        /// <returns>n-th number in Fib sequence</returns>
        public int GetFibNumber(int num)
        {
            //base case
            if (num == 0) return 0;
            else if (num == 1) return 1;

            return (GetFibNumber(num - 1) + GetFibNumber(num - 2));
        }

        /// <summary>
        /// linearly O(n) calculate n-th Fib number
        /// </summary>
        /// <param name="num"></param>
        /// <returns>n-th number in Fib sequence</returns>
        public int GetFibNumber(int num, bool linear)
        {
            //0 1 1 2 3 5 8 ...
            int a = 0, b = 1, prev = 0;

            while (num > 0)
            {
                prev = b;
                b = b + a;
                a = prev;
                num--;
            }
            return a;
        }
    }



    [TestFixture]
    public class FibonacciTests
    {
        private Fibonacci f;

        [SetUp]
        public void SetUp()
        {
            f = new Fibonacci();
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(3,2)]
        [TestCase(5,5)]
        public void ItShouldCalcFibRecursive(int num, int fib)
        {
            int result = f.GetFibNumber(num);

            Assert.That(result, Is.EqualTo(fib));
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(3, 2)]
        [TestCase(5, 5)]
        public void ItShouldCalcFibLinear(int num, int fib)
        {
            int result = f.GetFibNumber(num, true);

            Assert.That(result, Is.EqualTo(fib));
        }
    }
}
