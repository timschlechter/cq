using System.Collections.Generic;
using System.IO;
using CQ.HttpApi.JsonSerialization;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.JsonSerialization
{
    [TestFixture]
    public class SimpleJsonSerializerTests
    {
        [Test]
        public void Serialize_DynamicWithList()
        {
            var serializer = new SimpleJsonSerializer();

            using (var stream = new MemoryStream())
            {
                serializer.Serialize(new Some { Items = new List<int> {0, 1, 2}}, stream);
                stream.Position = 0;

                var actual = serializer.Deserialize(stream, typeof(Some));
            }
        }

        public class Some
        {
            public List<int> Items { get; set; }
        }
    }
}