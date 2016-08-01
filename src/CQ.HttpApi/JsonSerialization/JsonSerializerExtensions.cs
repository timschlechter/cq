using System;
using System.IO;

namespace CQ.HttpApi.JsonSerialization
{
    public static class JsonSerializerExtensions
    {
        public static object MakeStronglyTyped(this IJsonSerializer serializer, object source, Type targetType)
        {
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(source, stream);
                stream.Position = 0;
                return serializer.Deserialize(stream, targetType);
            }
        }

        public static string Serialize(this IJsonSerializer serializer, object value)
        {
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(value, stream);
                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}