using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JsonConverters.Collections.UnitTests.IEnumerableJsonConverterTests
{
    [TestClass]
    public class DeserializeShould
    {
        [TestMethod]
        public void DeserializeBooleanArrayToArrayOfBoolean()
        {
            var booleans = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(booleans);
            var deserialized = JsonConvert.DeserializeObject<bool[]>(serialized, new JsonConverter[] { new EnumerableJsonConverter(typeof(bool)) });
            booleans.Should().BeEquivalentTo(deserialized);
        }

        [TestMethod]
        public void DeserializeBooleanArrayToIEnumerableOfBoolean()
        {
            var booleans = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(booleans);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<bool>>(serialized, new JsonConverter[] { new EnumerableJsonConverter(typeof(bool)) });
            booleans.Should().BeEquivalentTo(deserialized);
        }

        [TestMethod]
        public void DeserializeBooleanArrayToIEnumerableOfObject()
        {
            var booleans = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(booleans);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<object>>(serialized, new JsonConverter[] { new EnumerableJsonConverter(typeof(bool)) });
            booleans.Should().BeEquivalentTo(deserialized);
        }

        [TestMethod]
        public void DeserializeBooleanArrayToListOfBoolean()
        {
            var booleans = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(booleans);
            var deserialized = JsonConvert.DeserializeObject<List<bool>>(serialized, new JsonConverter[] { new EnumerableJsonConverter(typeof(bool)) });
            booleans.Should().BeEquivalentTo(deserialized);
        }
    }
}
