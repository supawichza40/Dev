// **********************************************************************************
// The MIT License (MIT)
// 
// Copyright (c) 2014 Charlie Poole, Rob Prouse
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
// **********************************************************************************

#region Using Directives

using NUnit.Framework;

#endregion

namespace NUnit.TestData
{
    [TestFixture(Author = "Rob Prouse"), Author("Charlie Poole", "charlie@poole.org")]
    [Author("NUnit")]
    public class AuthorFixture
    {
        [Test(Author = "Rob Prouse")]
        public void Method()
        { }

        [Test]
        public void NoAuthorMethod()
        { }

        [Test]
        [Author("Rob Prouse")]
        public void SeparateAuthorMethod()
        { }

        [Test]
        [Author("Rob Prouse", "rob@prouse.org")]
        public void SeparateAuthorWithEmailMethod()
        { }

        [Test, Author("Rob Prouse")]
        [TestCase(5, Author = "Charlie Poole")]
        public void TestCaseWithAuthor(int x)
        { }

        [Test, Author("Rob Prouse", "rob@prouse.org")]
        [Author("Charlie Poole", "charlie@poole.org"), Author("NUnit")]
        public void TestMethodMultipleAuthors()
        { }

    }
}