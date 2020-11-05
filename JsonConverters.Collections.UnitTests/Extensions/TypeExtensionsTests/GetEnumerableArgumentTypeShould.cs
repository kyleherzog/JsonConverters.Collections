using System;
using System.Collections.Generic;
using JsonConverters.Collections.Extensions;
using JsonConverters.Collections.UnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonConverters.Collections.UnitTests.Extensions.TypeExtensionsTests
{
    [TestClass]
    public class GetEnumerableArgumentTypeShould
    {
        [TestMethod]
        public void ReturnTypeGivenClassImplementingInterface()
        {
            var type = typeof(List<bool>).GetEnumerablArgumentType();
            Assert.AreEqual(typeof(bool), type);
        }

        [TestMethod]
        public void ReturnTypeGivenDirectIEnumerable()
        {
            var type = typeof(IEnumerable<string>).GetEnumerablArgumentType();
            Assert.AreEqual(typeof(string), type);
        }

        [TestMethod]
        public void ReturnTypeGivenInheritedIterface()
        {
            var type = typeof(IList<double>).GetEnumerablArgumentType();
            Assert.AreEqual(typeof(double), type);
        }

        [TestMethod]
        public void ReturnTypeGivenInhertingType()
        {
            var type = typeof(BooleanCollection).GetEnumerablArgumentType();
            Assert.AreEqual(typeof(bool), type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThowExceptionGivenNullType()
        {
            TypeExtensions.GetEnumerablArgumentType(null);
            Assert.Fail("An exception should have been thrown.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowExceptionGivenNonEnumerableType()
        {
            typeof(double).GetEnumerablArgumentType();
            Assert.Fail("An exception should have been thrown.");
        }
    }
}
