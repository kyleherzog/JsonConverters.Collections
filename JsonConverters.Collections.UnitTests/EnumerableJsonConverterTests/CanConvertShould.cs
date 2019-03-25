using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JsonConverters.Collections.UnitTests.EnumerableJsonConverterTests
{
    [TestClass]
    public class CanConvertShould
    {
        [TestMethod]
        public void ReturnsFalseGivenObjectTypeGenericArgumentIsNotSubTypeOfItemType()
        {
            var converter = new EnumerableJsonConverter(typeof(bool));
            Assert.IsFalse(converter.CanConvert(typeof(List<string>)));
        }

        [TestMethod]
        public void ReturnsFalseGivenObjectTypeIsNotEnumerableType()
        {
            var converter = new EnumerableJsonConverter(typeof(bool));
            Assert.IsFalse(converter.CanConvert(typeof(bool)));
        }

        [TestMethod]
        public void ReturnsTrueGivenObjectTypeGenericArgumentIsSubTypeOfItemType()
        {
            var converter = new EnumerableJsonConverter(typeof(bool));
            Assert.IsTrue(converter.CanConvert(typeof(List<object>)));
        }

        [TestMethod]
        public void ReturnsTrueGivenObjectTypeIsArrayOfMatchingItemType()
        {
            var converter = new EnumerableJsonConverter(typeof(bool));
            Assert.IsTrue(converter.CanConvert(typeof(bool[])));
        }

        [TestMethod]
        public void ReturnsTrueGivenObjectTypeIsIEnumerableOfMatchingItemType()
        {
            var converter = new EnumerableJsonConverter(typeof(bool));
            Assert.IsTrue(converter.CanConvert(typeof(IEnumerable<bool>)));
        }

        [TestMethod]
        public void ReturnsTrueGivenObjectTypeIsIEnumerableOfSubItemType()
        {
            var converter = new EnumerableJsonConverter(typeof(bool));
            Assert.IsTrue(converter.CanConvert(typeof(IEnumerable<object>)));
        }

        [TestMethod]
        public void ReturnsTrueGivenObjectTypeIsListOfMatchingItemType()
        {
            var converter = new EnumerableJsonConverter(typeof(bool));
            Assert.IsTrue(converter.CanConvert(typeof(List<bool>)));
        }
    }
}
