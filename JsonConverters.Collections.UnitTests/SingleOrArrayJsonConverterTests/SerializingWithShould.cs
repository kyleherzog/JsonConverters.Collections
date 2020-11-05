using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JsonConverters.Collections.UnitTests.SingleOrArrayJsonConverterTests
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
        public void SerializeAsSingleValueGivenSingleValueArray()
        {
            var values = new bool[] { true };
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
