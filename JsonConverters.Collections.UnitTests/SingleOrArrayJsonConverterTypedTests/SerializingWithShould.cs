using System.Collections.Generic;
using System.Linq;
using JsonConverters.Collections.UnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JsonConverters.Collections.UnitTests.SingleOrArrayJsonConverterTypedTests
{
    [TestClass]
    public class SerializingWithShould
    {
        [TestMethod]
        public void SerializeAsArrayGivenMultipleValueArray()
        {
            var values = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(values, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            var expected = JsonConvert.SerializeObject(values);
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeAsArrayGivenMultipleValueArrayWithClassAttribute()
        {
            var values = new SingleOrList<string> { "Hello", "world", "!!!" };
            var serialized = JsonConvert.SerializeObject(values, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            var expected = JsonConvert.SerializeObject(new List<string>(values));
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeAsSingleValueGivenSingleValueArray()
        {
            var values = new bool[] { true };
            var serialized = JsonConvert.SerializeObject(values, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            var expected = JsonConvert.SerializeObject(values.First());
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeAsSingleValueGivenSingleValueArrayWithClassAttribute()
        {
            var values = new SingleOrList<string> { "Hi" };
            var serialized = JsonConvert.SerializeObject(values, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            var expected = JsonConvert.SerializeObject(values.First());
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeProperlyGivenNullValue()
        {
            string value = null;
            var serialized = JsonConvert.SerializeObject(value, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            var expected = JsonConvert.SerializeObject(value);
            Assert.AreEqual(expected, serialized);
        }
    }
}
