// ***********************************************************************
// Copyright (c) 2021 Charlie Poole, Rob Prouse
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

namespace NUnit.TestData.LifeCycleTests
{
    public static class AssemblyLevelFixtureLifeCycleTest
    {
        public const string Code = @"
            using NUnit.Framework;

            [assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]

            [TestFixture]
            public class FixtureUnderTest
            {
                private int _value;

                [Test]
                public void Test1()
                {
                    Assert.AreEqual(0, _value++);
                }

                [Test]
                public void Test2()
                {
                    Assert.AreEqual(0, _value++);
                }
            }
            ";
    }

    public static class AssemblyLevelLifeCycleTestFixtureSourceTest
    {
        public const string Code = @"
            using NUnit.Framework;

            [assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]

            [TestFixtureSource(""FixtureArgs"")]
            public class FixtureUnderTest
            {
                private readonly int _initialValue;
                private int _value;

                public FixtureUnderTest(int num)
                {
                    _initialValue = num;
                    _value = num;
                }

                public static int[] FixtureArgs()
                {
                    return new int[] { 1, 42 };
                }

                [Test]
                public void Test1()
                {
                    Assert.AreEqual(_initialValue, _value++);
                }

                [Test]
                public void Test2()
                {
                    Assert.AreEqual(_initialValue, _value++);
                }
            }
            ";
    }

    public static class AssemblyLevelLifeCycleFixtureWithTestCasesTest
    {
        public const string Code = @"
            using NUnit.Framework;

            [assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]

            public class FixtureUnderTest
            {
                private int _counter;

                [TestCase(0)]
                [TestCase(1)]
                [TestCase(2)]
                [TestCase(3)]
                public void Test(int _)
                {
                    Assert.AreEqual(0, _counter++);
                }
            }
            ";
    }

    public static class AssemblyLevelLifeCycleFixtureWithTestCaseSourceTest
    {
        public const string Code = @"
            using NUnit.Framework;

            [assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]

            public class FixtureUnderTest
            {
                private int _counter;

                public static int[] Args()
                {
                    return new int[] { 1, 42 };
                }

                [TestCaseSource(""Args"")]
                public void Test(int _)
                {
                    Assert.AreEqual(0, _counter++);
                }
            }
            ";
    }

    public static class AssemblyLevelLifeCycleFixtureWithValuesTest
    {
        public const string Code = @"
            using NUnit.Framework;

            [assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]

            public class FixtureUnderTest
            {
                private int _counter;

                [Test]
                public void Test([Values] bool? _)
                {
                    Assert.AreEqual(0, _counter++);
                }
            }
            ";
    }

    public static class AssemblyLevelLifeCycleFixtureWithTheoryTest
    {
        public const string Code = @"
            using NUnit.Framework;

            [assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]

            public class FixtureUnderTest
            {
                private int _counter;

                [Theory]
                public void Test(bool? _)
                {
                    Assert.AreEqual(0, _counter++);
                }
            }
            ";
    }
}
