using System.Collections.Generic;
using System.IO;
using CQ.HttpApi.JsonSerialization;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.JsonSerialization
{
    [TestFixture]
    public class SimpleJsonSerializerTests
    {
        public class Some
        {
            public List<int> Items { get; set; }
        }

        [Test]
        public void Deserialize_SerializedList_HasSameItems()
        {
            var serializer = new DefaultJsonSerializer();

            var serialized = serializer.Serialize(new Some { Items = new List<int> { 0, 1, 2 } });
            var deserialized = serializer.Deserialize<Some>(serialized);
            
            Assert.AreEqual(3, deserialized.Items.Count);
            Assert.AreEqual(0, deserialized.Items[0]);
            Assert.AreEqual(1, deserialized.Items[1]);
            Assert.AreEqual(2, deserialized.Items[2]);
        }

        [Test]
        public void Serialize_StringValueWithDoubleQuote_EscapesValue()
        {
            var serializer = new DefaultJsonSerializer();

            var serialized = serializer.Serialize("\"");

            Assert.AreEqual("\"\\\"\"", serialized);
        }
    }
}