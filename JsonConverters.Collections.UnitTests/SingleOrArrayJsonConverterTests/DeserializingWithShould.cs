using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using JsonConverters.Collections.UnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JsonConverters.Collections.UnitTests.SingleOrArrayJsonConverterTests
{
    [TestClass]
    public class DeserializingWithShould
    {
        [TestMethod]
        public void DeserializeInheritedListGivenClassAttributionAndSingleItems()
        {
            var oddList = new SingleOrList<string> { "OnlyOne" };
            var serialized = JsonConvert.SerializeObject(oddList);
            var deserialized = JsonConvert.DeserializeObject<SingleOrList<string>>(serialized);
            deserialized.Should().BeEquivalentTo(oddList);
        }

        [TestMethod]
        public void DeserializeInheritedListGivenClassAttributionAndMultipleItems()
        {
            var oddList = new SingleOrList<string> { "1", "3", "5" };
            var serialized = JsonConvert.SerializeObject(oddList);
            var deserialized = JsonConvert.DeserializeObject<SingleOrList<string>>(serialized);
            deserialized.Should().BeEquivalentTo(oddList);
        }

        [TestMethod]
        public void DeserializeBooleanArrayToArrayOfBoolean()
        {
            var booleans = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(booleans);
            var deserialized = JsonConvert.DeserializeObject<bool[]>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            deserialized.Should().BeEquivalentTo(booleans);
        }

        [TestMethod]
        public void DeserializeBooleanArrayToIEnumerableOfBoolean()
        {
            var booleans = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(booleans);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<bool>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            deserialized.Should().BeEquivalentTo(booleans);
        }

        [TestMethod]
        public void DeserializeBooleanArrayToIEnumerableOfObject()
        {
            var booleans = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(booleans);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<object>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            deserialized.Should().BeEquivalentTo(booleans);
        }

        [TestMethod]
        public void DeserializeBooleanArrayToListOfBoolean()
        {
            var booleans = new bool[] { true, false, true };
            var serialized = JsonConvert.SerializeObject(booleans);
            var deserialized = JsonConvert.DeserializeObject<List<bool>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            deserialized.Should().BeEquivalentTo(booleans);
        }

        [TestMethod]
        public void DeserializeBooleanToArrayOfBoolean()
        {
            var value = true;
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<bool[]>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            Assert.AreEqual(1, deserialized.Length);
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeBooleanToIEnumerableOfBoolean()
        {
            var value = true;
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<bool>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            Assert.AreEqual(1, deserialized.Count());
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeBooleanToIEnumerableOfObject()
        {
            var value = true;
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<object>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            Assert.AreEqual(1, deserialized.Count());
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeBooleanToListOfBoolean()
        {
            var value = true;
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<List<bool>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<bool>() });
            Assert.AreEqual(1, deserialized.Count);
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeIntArrayToArrayOfInt()
        {
            var values = new int[] { 432, 789, 2 };
            var serialized = JsonConvert.SerializeObject(values);
            var deserialized = JsonConvert.DeserializeObject<int[]>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<int>() });
            deserialized.Should().BeEquivalentTo(values);
        }

        [TestMethod]
        public void DeserializeIntArrayToIEnumerableOfInt()
        {
            var values = new int[] { 432, 789, 2 };
            var serialized = JsonConvert.SerializeObject(values);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<int>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<int>() });
            deserialized.Should().BeEquivalentTo(values);
        }

        [TestMethod]
        public void DeserializeIntArrayToIEnumerableOfObject()
        {
            var values = new int[] { 432, 789, 2 };
            var serialized = JsonConvert.SerializeObject(values);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<object>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<int>() });
            deserialized.Should().BeEquivalentTo(values);
        }

        [TestMethod]
        public void DeserializeIntArrayToListOfInt()
        {
            var values = new int[] { 432, 789, 2 };
            var serialized = JsonConvert.SerializeObject(values);
            var deserialized = JsonConvert.DeserializeObject<List<int>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<int>() });
            deserialized.Should().BeEquivalentTo(values);
        }

        [TestMethod]
        public void DeserializeIntToArrayOfInt()
        {
            var value = 2564;
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<int[]>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<int>() });
            Assert.AreEqual(1, deserialized.Length);
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeIntToIEnumerableOfInt()
        {
            var value = 23;
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<int>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<int>() });
            Assert.AreEqual(1, deserialized.Count());
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeIntToIEnumerableOfObject()
        {
            var value = 42325;
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<object>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<int>() });
            Assert.AreEqual(1, deserialized.Count());
            deserialized.First().Should().BeEquivalentTo(value); // this will actually be an Int64 so all we can compare is equivalence.
        }

        [TestMethod]
        public void DeserializeIntToListOfInt()
        {
            var value = 5553;
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<List<int>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<int>() });
            Assert.AreEqual(1, deserialized.Count);
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeStringArrayToArrayOfStrings()
        {
            var values = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            var serialized = JsonConvert.SerializeObject(values);
            var deserialized = JsonConvert.DeserializeObject<string[]>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<string>() });
            deserialized.Should().BeEquivalentTo(values);
        }

        [TestMethod]
        public void DeserializeStringArrayToIEnumerableOfObject()
        {
            var values = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            var serialized = JsonConvert.SerializeObject(values);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<object>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<string>() });
            deserialized.Should().BeEquivalentTo(values);
        }

        [TestMethod]
        public void DeserializeStringArrayToIEnumerableOfString()
        {
            var values = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            var serialized = JsonConvert.SerializeObject(values);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<string>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<string>() });
            deserialized.Should().BeEquivalentTo(values);
        }

        [TestMethod]
        public void DeserializeStringArrayToListOfString()
        {
            var values = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            var serialized = JsonConvert.SerializeObject(values);
            var deserialized = JsonConvert.DeserializeObject<List<string>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<string>() });
            deserialized.Should().BeEquivalentTo(values);
        }

        [TestMethod]
        public void DeserializeStringToArrayOfString()
        {
            var value = Guid.NewGuid().ToString();
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<string[]>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<string>() });
            Assert.AreEqual(1, deserialized.Length);
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeStringToIEnumerableOfObject()
        {
            var value = Guid.NewGuid().ToString();
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<object>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<string>() });
            Assert.AreEqual(1, deserialized.Count());
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeStringToIEnumerableOfString()
        {
            var value = Guid.NewGuid().ToString();
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<string>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<string>() });
            Assert.AreEqual(1, deserialized.Count());
            Assert.AreEqual(value, deserialized.First());
        }

        [TestMethod]
        public void DeserializeStringToListOfString()
        {
            var value = Guid.NewGuid().ToString();
            var serialized = JsonConvert.SerializeObject(value);
            var deserialized = JsonConvert.DeserializeObject<List<string>>(serialized, new JsonConverter[] { new SingleOrArrayJsonConverter<string>() });
            Assert.AreEqual(1, deserialized.Count);
            Assert.AreEqual(value, deserialized.First());
        }
    }
}
