using System;
using System.IO;

namespace CQ.HttpApi.JsonSerialization
{
    public interface IJsonSerializer
    {
        object Deserialize(Stream stream, Type type);
        void Serialize(object value, Stream stream);
    }
}