using System;
using System.Collections.Generic;
using JsonConverters.Collections.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonConverters.Collections.UnitTests.Extensions.TypeExtensionsTests
{
    [TestClass]
    public class IsIEnumerableShould
    {
        [TestMethod]
        public void ReturnFalseGivenItnerfaceDoesNotImplementIEnumberable()
        {
            Assert.IsFalse(typeof(IComparable).IsIEnumerable());
        }

        [TestMethod]
        public void ReturnFalseGivenNonEnumerableType()
        {
            Assert.IsFalse(typeof(bool).IsIEnumerable());
        }

        [TestMethod]
        public void ReturnTrueGivenClassImplementingIEnumerable()
        {
            Assert.IsTrue(typeof(List<string>).IsIEnumerable());
        }

        [TestMethod]
        public void ReturnTrueGivenDirectIEnumerableType()
        {
            Assert.IsTrue(typeof(IEnumerable<string>).IsIEnumerable());
        }

        [TestMethod]
        public void ReturnTrueGivenInterfaceImplementingIEnumerable()
        {
            Assert.IsTrue(typeof(IList<bool>).IsIEnumerable());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThowExceptionGivenNullType()
        {
            TypeExtensions.IsIEnumerable(null);
            Assert.Fail("An exception should have been thrown.");
        }
    }
}
