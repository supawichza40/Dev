// ***********************************************************************
// Copyright (c) 2015 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit.TestData.TestContextData
{
    [TestFixture]
    public class TestStateRecordingFixture
    {
        public string StateList;

        public bool TestFailure;
        public bool TestInconclusive;
        public bool SetUpFailure;
        public bool SetUpIgnore;

        [SetUp]
        public void SetUp()
        {
            StateList = TestContext.CurrentContext.Result.Outcome + "=>";

            if (SetUpFailure)
                Assert.Fail("Failure in SetUp");
            if (SetUpIgnore)
                Assert.Ignore("Ignored in SetUp");
        }

        [Test]
        public void TheTest()
        {
            StateList += TestContext.CurrentContext.Result.Outcome;

            if (TestFailure)
                Assert.Fail("Deliberate failure");
            if (TestInconclusive)
                Assert.Inconclusive("Inconclusive test");
        }

        [TearDown]
        public void TearDown()
        {
            StateList += "=>" + TestContext.CurrentContext.Result.Outcome;
        }
    }

    public class AssertionResultFixture
    {
        public IEnumerable<AssertionResult> Assertions;

        public void ThreeAsserts_TwoFailed()
        {
            Assert.Multiple(() =>
            {
                Assert.That(2 + 2, Is.EqualTo(5));
                Assert.That(2 + 2, Is.EqualTo(4));
                Assert.That(2 + 2, Is.EqualTo(5));

                Assertions = TestContext.CurrentContext.Result.Assertions;
            });
        }

        public void WarningPlusFailedAssert()
        {
            Warn.Unless(2 + 2, Is.EqualTo(5));


            Assert.Multiple(() =>
            {
                Assert.That(2 + 2, Is.EqualTo(5));

                Assertions = TestContext.CurrentContext.Result.Assertions;
            });
        }
    }

    [TestFixture]
    public class TestTestContextInTearDown
    {
        public int FailCount { get; private set; }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }

        [Test]
        public void FailingTest()
        {
            Assert.Fail("Deliberate failure");
        }

        [TearDown]
        public void TearDown()
        {
            FailCount = TestContext.CurrentContext.Result.FailCount;
            Message = TestContext.CurrentContext.Result.Message;
            StackTrace = TestContext.CurrentContext.Result.StackTrace;
        }
    }

    [TestFixture]
    public class TestTestContextInOneTimeTearDown
    {
        public int PassCount { get; private set; }
        public int FailCount { get; private set; }
        public int WarningCount { get; private set; }
        public int SkipCount { get; private set; }
        public int InconclusiveCount { get; private set; }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }

        [Test]
        public void FailingTest()
        {
            Assert.Fail("Deliberate failure");
        }

        [Test]
        public void PassingTest()
        {
            Assert.Pass();
        }

        [Test]
        public void AnotherPassingTest()
        {
            Assert.Pass();
        }

        [Test]
        public void IgnoredTest()
        {
            Assert.Ignore("I don't want to run this test");
        }

        [Test]
        public void IgnoredTestTwo()
        {
            Assert.Ignore("I don't want to run this test either");
        }

        [Test]
        public void IgnoredTestThree()
        {
            Assert.Ignore("Nor do I want to run this test");
        }

        [Test]
        public void AssumeSomething()
        {
            Assume.That( false );
        }

        [Test]
        public void AssumeSomethingElse()
        {
            Assume.That(false);
        }

        [Test]
        public void NeverAssume()
        {
            Assume.That(false);
        }

        [Test]
        public void AssumeTheWorldIsFlat()
        {
            Assume.That(false);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            PassCount = TestContext.CurrentContext.Result.PassCount;
            FailCount = TestContext.CurrentContext.Result.FailCount;
            WarningCount = TestContext.CurrentContext.Result.WarningCount;
            SkipCount = TestContext.CurrentContext.Result.SkipCount;
            InconclusiveCount = TestContext.CurrentContext.Result.InconclusiveCount;
            Message = TestContext.CurrentContext.Result.Message;
        }
    }
}
