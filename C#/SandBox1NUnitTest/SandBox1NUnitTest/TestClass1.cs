using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SandBox1NUnitTest
{
    [TestFixture]
    public class TestClass1
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            IsTheSame("10", "10");
            Normal.IsTheSame("10", "10");
        }
    }
}
