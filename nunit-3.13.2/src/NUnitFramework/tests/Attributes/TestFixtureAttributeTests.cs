// ***********************************************************************
// Copyright (c) 2008 Charlie Poole, Rob Prouse
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

using System;

namespace NUnit.Framework.Attributes
{
    public class TestFixtureAttributeTests
    {
        static readonly object[] fixtureArgs = { 10, 20, "Charlie" };
        static readonly Type[] typeArgs = { typeof(int), typeof(string) };
        static readonly object[] combinedArgs = { typeof(int), typeof(string), 10, 20, "Charlie" };

        [Test]
        public void ConstructWithoutArguments()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute();
            Assert.That(attr.Arguments.Length == 0);
            Assert.That(attr.TypeArgs.Length == 0);
        }

        [Test]
        public void ConstructWithFixtureArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute(fixtureArgs);
            Assert.That(attr.Arguments, Is.EqualTo( fixtureArgs ) );
            Assert.That(attr.TypeArgs.Length == 0 );
        }

        [Test]
        public void ConstructWithJustTypeArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute(typeArgs);
            Assert.That(attr.Arguments.Length == 2);
            Assert.That(attr.TypeArgs.Length == 0);
        }

        [Test]
        public void ConstructWithNoArgumentsAndSetTypeArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute();
            attr.TypeArgs = typeArgs;
            Assert.That(attr.Arguments.Length == 0);
            Assert.That(attr.TypeArgs, Is.EqualTo(typeArgs));
        }

        [Test]
        public void ConstructWithFixtureArgsAndSetTypeArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute(fixtureArgs);
            attr.TypeArgs = typeArgs;
            Assert.That(attr.Arguments, Is.EqualTo(fixtureArgs));
            Assert.That(attr.TypeArgs, Is.EqualTo(typeArgs));
        }

        [Test]
        public void ConstructWithCombinedArgs()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute(combinedArgs);
            Assert.That(attr.Arguments, Is.EqualTo(combinedArgs));
            Assert.That(attr.TypeArgs.Length, Is.EqualTo(0));
        }

        [Test]
        public void ConstructWithWeakTypedNullArgument()
        {
            TestFixtureAttribute attr = new TestFixtureAttribute(null);
            Assert.That(attr.Arguments, Is.Not.Null);
            Assert.That(attr.Arguments[0], Is.Null);
            Assert.That(attr.TypeArgs, Is.Not.Null);
        }
    }
}