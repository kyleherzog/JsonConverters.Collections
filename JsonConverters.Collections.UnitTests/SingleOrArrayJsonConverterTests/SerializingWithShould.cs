using System.Collections.Generic;
using System.Linq;
using JsonConverters.Collections.UnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JsonConverters.Collections.UnitTests.SingleOrArrayJsonConverterTests
{
    [TestClass]
    public class SerializingWithShould
    {
        [TestMethod]
        public void SerializeAsArrayGivenNestedListWithMultipleValues()
        {
            var list = new NestedValueOrList();
            list.Items.AddRange(new int[] { 1, 2, 3 });
            var expected = JsonConvert.SerializeObject(list.Items);
            var serialized = JsonConvert.SerializeObject(list, new JsonConverter[] { new SingleOrArrayJsonConverter() });
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeAsSingleValueGivenNestedListWithSingleValues()
        {
            var list = new NestedValueOrList();
            list.Items.Add(1);
            var expected = "1";
            var serialized = JsonConvert.SerializeObject(list, new JsonConverter[] { new SingleOrArrayJsonConverter() });
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeAsArrayGivenMultipleValueArray()
        {
            var values = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(values, new JsonConverter[] { new SingleOrArrayJsonConverter() });
            var expected = JsonConvert.SerializeObject(values);
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeAsArrayGivenMultipleValueArrayWithClassAttribute()
        {
            var values = new SingleOrList<string> { "Hello", "world", "!!!" };
            var serialized = JsonConvert.SerializeObject(values, new JsonConverter[] { new SingleOrArrayJsonConverter() });
            var expected = JsonConvert.SerializeObject(new List<string>(values));
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeAsSingleValueGivenSingleValueArray()
        {
            var values = new bool[] { true };
            var serialized = JsonConvert.SerializeObject(values, new JsonConverter[] { new SingleOrArrayJsonConverter() });
            var expected = JsonConvert.SerializeObject(values.First());
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeAsSingleValueGivenSingleValueArrayWithClassAttribute()
        {
            var values = new SingleOrList<string> { "Hi" };
            var serialized = JsonConvert.SerializeObject(values, new JsonConverter[] { new SingleOrArrayJsonConverter() });
            var expected = JsonConvert.SerializeObject(values.First());
            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void SerializeProperlyGivenNullValue()
        {
            string value = null;
            var serialized = JsonConvert.SerializeObject(value, new JsonConverter[] { new SingleOrArrayJsonConverter() });
            var expected = JsonConvert.SerializeObject(value);
            Assert.AreEqual(expected, serialized);
        }
    }
}
