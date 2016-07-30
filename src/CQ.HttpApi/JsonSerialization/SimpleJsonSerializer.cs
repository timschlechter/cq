using System;
using System.IO;
using System.Text;

namespace CQ.HttpApi.JsonSerialization
{
    public class SimpleJsonSerializer : IJsonSerializer
    {
        public void Serialize(object value, Stream stream)
        {
            using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
            {
                var serialized = SimpleJson.SimpleJson.SerializeObject(value);
                writer.Write(serialized);
            }
        }

        public object Deserialize(Stream stream, Type type)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var serialized = reader.ReadToEnd();
                return SimpleJson.SimpleJson.DeserializeObject(serialized, type);
            }
        }
    }
}