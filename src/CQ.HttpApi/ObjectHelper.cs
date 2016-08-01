using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;

namespace CQ.HttpApi
{
    public class ObjectHelper
    {
        private static readonly string Separator = ".";

        public static ILookup<string, object> Flatten(object value)
        {
            return Flatten(value, null).ToLookup(kvp => kvp.Key, kvp => kvp.Value);
        }

        private static IList<KeyValuePair<string, object>> Flatten(object value, string namePrefix)
        {
            if (value == null)
            {
                return null;
            }

            var result = new List<KeyValuePair<string, dynamic>>();

            foreach (var pi in value.GetType().GetProperties())
            {
                var propertyValue = pi.GetValue(value);
                var propertyType = pi.PropertyType;
                var propertyName = pi.Name;

                if (propertyType == typeof (object))
                {
                    propertyType = propertyValue.GetType();
                }

                if (propertyValue == null)
                {
                    // skip
                }
                else if (propertyType.IsValueType || propertyType == typeof (string))
                {
                    result.Add(new KeyValuePair<string, dynamic>(namePrefix + propertyName, propertyValue));
                }
                else if (propertyType.GetInterface("IEnumerable") != null)
                {
                    foreach (var item in (IEnumerable) propertyValue)
                    {
                        result.Add(new KeyValuePair<string, dynamic>(namePrefix + propertyName + "[]", item));
                    }
                }
                else
                {
                    var children = Flatten(propertyValue, propertyName + Separator);
                    if (children != null)
                    {
                        foreach (var child in children)
                        {
                            result.Add(new KeyValuePair<string, dynamic>(namePrefix + child.Key, child.Value));
                        }
                    }
                }
            }

            return result;
        }

        public static dynamic Expand(IEnumerable<KeyValuePair<string, object>> valueCollection)
        {
            var valueCollectionLookup = valueCollection.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
            var result = new ExpandoObject() as IDictionary<string, object>;

            var complexProperties = new Dictionary<string, IList<KeyValuePair<string, object>>>();

            foreach (var group in valueCollectionLookup)
            {
                var key = group.Key;
                var parts = key.Split(Separator[0]);


                var value = valueCollectionLookup[key].First();
                if (parts.Length == 1)
                {
                    result.Add(key, value);
                }
                else
                {
                    var propertyName = parts[0];

                    if (!complexProperties.ContainsKey(propertyName))
                    {
                        complexProperties.Add(propertyName, new List<KeyValuePair<string, object>>());
                    }
                    complexProperties[propertyName].Add(new KeyValuePair<string, object>(string.Join(Separator, parts.Skip(1)), value));
                }
            }

            foreach (var kvp in complexProperties)
            {
                result.Add(kvp.Key, Expand(kvp.Value));
            }

            return result;
        }

        public static object ExpandQueryString(string queryString)
        {
            var parameters = queryString
                .Replace("?", "")
                .Split('&')
                .Where(item => !string.IsNullOrEmpty(item))
                .Select(item =>
                {
                    var parts = item.Split('=');
                    var name = parts[0];


                    var value = parts[1];
                    return new KeyValuePair<string, object>(name, WebUtility.UrlDecode(value));
                });

            return Expand(parameters);
        }
    }
}