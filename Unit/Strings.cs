using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Unit
{
    public class Strings
    {
        public string ReverseString(string s)
        {
            if(s.Length == 0)
            {
                throw new ArgumentException("Input string should not be empty!");
            }
            //base case
            if (s.Length == 1) return s;

            //run recursively
            return s[s.Length - 1] + ReverseString(s.Substring(0, s.Length - 1));
        }
    }

    [TestFixture]
    public class StringsTests
    {
        private Strings s;

        [SetUp]
        public void SetUp()
        {
            s = new Strings();
        }

        [Test]
        public void StringShouldEqualItselfReversed()
        {
            string res = s.ReverseString("abc");
            Assert.AreEqual(res, "cba");
        }

        [Test]
        public void StringShouldNotBeEmpty()
        {
           Assert.That(() => s.ReverseString(""), Throws.TypeOf <ArgumentException> ());
        }
    }
}
