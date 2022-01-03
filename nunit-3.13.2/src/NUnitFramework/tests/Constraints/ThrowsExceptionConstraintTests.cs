// ***********************************************************************
// Copyright (c) 2012 Charlie Poole, Rob Prouse
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

using NUnit.Framework.Internal;
using NUnit.TestUtilities;

#if TASK_PARALLEL_LIBRARY_API
using System;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace NUnit.Framework.Constraints
{
    [TestFixture]
    public class ThrowsExceptionConstraintTests : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            TheConstraint = new ThrowsExceptionConstraint();
            ExpectedDescription = "an exception to be thrown";
            StringRepresentation = "<throwsexception>";
        }

        [Test]
        public void SucceedsWithNonVoidReturningFunction()
        {
            var constraintResult = TheConstraint.ApplyTo(TestDelegates.ThrowsInsteadOfReturns);
            if (!constraintResult.IsSuccess)
            {
                MessageWriter writer = new TextMessageWriter();
                constraintResult.WriteMessageTo(writer);
                Assert.Fail(writer.ToString());
            }
        }

        static object[] SuccessData = new object[]
        {
            new TestDelegate( TestDelegates.ThrowsArgumentException )
        };

        static object[] FailureData = new object[]
        {
            new TestCaseData( new TestDelegate( TestDelegates.ThrowsNothing ), "no exception thrown" ),
        };

#if TASK_PARALLEL_LIBRARY_API
        [Test]
        public static void CatchesAsyncException()
        {
            Assert.That(async () => await AsyncTestDelegates.ThrowsArgumentExceptionAsync(), Throws.Exception);
        }
        
        [Test]
        public static void CatchesAsyncTaskOfTException()
        {
            Assert.That<Task<int>>(async () =>
            {
                await AsyncTestDelegates.Delay(5);
                throw new Exception();
            }, Throws.Exception);
        }

        [Test]
        public static void CatchesSyncException()
        {
            Assert.That(() => AsyncTestDelegates.ThrowsArgumentException(), Throws.Exception);
        }

        [Test]
        public static void CatchesSyncTaskOfTException()
        {
            Assert.That<Task<int>>(() =>
            {
                throw new Exception();
            }, Throws.Exception);
        }
#endif
    }
}
