using System;
using System.IO;

namespace CQ.HttpApi.JsonSerialization
{
    public static class JsonSerializerExtensions
    {
        public static object MakeTyped(this IJsonSerializer serializer, object source, Type targetType)
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

        public static object Deserialize(this IJsonSerializer serializer, string json, Type type)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;

                    return serializer.Deserialize(stream, type);
                }
            }
        }

        public static T Deserialize<T>(this IJsonSerializer serializer, string json)
        {
            return (T) serializer.Deserialize(json, typeof(T));
        }

        public static T Deserialize<T>(this IJsonSerializer serializer, Stream stream)
        {
            return (T) serializer.Deserialize(stream, typeof(T));
        }
    }
}