// ***********************************************************************
// Copyright (c) 2018 Charlie Poole, Rob Prouse
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

#if TASK_PARALLEL_LIBRARY_API
using System;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using NUnit.TestUtilities;

namespace NUnit.Framework
{
    partial class AsyncExecutionApiAdapter
    {
        private static void ExecuteFixture(Type fixtureType, AsyncTestDelegate asyncUserCode)
        {
            TestBuilder.RunTest(
                new NUnitTestFixtureBuilder().BuildFrom(new TypeWrapper(fixtureType), PreFilter.Empty, new TestFixtureData(asyncUserCode))
            ).AssertPassed();
        }

        private sealed class TaskReturningTestMethodAdapter : AsyncExecutionApiAdapter
        {
            public override void Execute(AsyncTestDelegate asyncUserCode)
            {
                ExecuteFixture(
                    typeof(TestData.AsyncExecutionApiAdapter.TaskReturningTestMethodFixture),
                    asyncUserCode);
            }

            public override string ToString() => "[Test] Task TestMethod() { ??? }";
        }

        private sealed class TaskReturningSetUpAdapter : AsyncExecutionApiAdapter
        {
            public override void Execute(AsyncTestDelegate asyncUserCode)
            {
                ExecuteFixture(
                    typeof(TestData.AsyncExecutionApiAdapter.TaskReturningSetUpFixture),
                    asyncUserCode);
            }

            public override string ToString() => "[SetUp] Task SetUp() { ??? }";
        }

        private sealed class TaskReturningTearDownAdapter : AsyncExecutionApiAdapter
        {
            public override void Execute(AsyncTestDelegate asyncUserCode)
            {
                ExecuteFixture(
                    typeof(TestData.AsyncExecutionApiAdapter.TaskReturningTearDownFixture),
                    asyncUserCode);
            }

            public override string ToString() => "[TearDown] Task TearDown() { ??? }";
        }

        private sealed class TaskReturningOneTimeSetUpAdapter : AsyncExecutionApiAdapter
        {
            public override void Execute(AsyncTestDelegate asyncUserCode)
            {
                ExecuteFixture(
                    typeof(TestData.AsyncExecutionApiAdapter.TaskReturningOneTimeSetUpFixture),
                    asyncUserCode);
            }

            public override string ToString() => "[OneTimeSetUp] Task OneTimeSetUp() { ??? }";
        }

        private sealed class TaskReturningOneTimeTearDownAdapter : AsyncExecutionApiAdapter
        {
            public override void Execute(AsyncTestDelegate asyncUserCode)
            {
                ExecuteFixture(
                    typeof(TestData.AsyncExecutionApiAdapter.TaskReturningOneTimeTearDownFixture),
                    asyncUserCode);
            }

            public override string ToString() => "[OneTimeTearDown] Task OneTimeTearDown() { ??? }";
        }
    }
}
#endif
