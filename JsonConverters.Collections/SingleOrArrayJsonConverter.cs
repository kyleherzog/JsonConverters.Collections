using System;
using System.Collections.Generic;
using System.Linq;
using JsonConverters.Collections.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConverters.Collections
{
    /// <summary>
    /// Provides serialization support for collections that are serialized as an array when there are multiple
    /// values, but when there is only one value only the value itself is serialized.
    /// </summary>
    /// <typeparam name="T">The type of values stored in the collection.</typeparam>
    public class SingleOrArrayJsonConverter<T> : JsonConverter
    {
        /// <summary>
        /// Use a privately create serializer so we don't re-enter into CanConvert and cause a Newtonsoft exception.
        /// </summary>
        private readonly JsonSerializer unregisteredConvertersSerializer = new JsonSerializer();

        /// <inheritdoc/>
        public override bool CanRead => true;

        /// <inheritdoc/>
        public override bool CanWrite => true;

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            var isIEnumerable = objectType.IsIEnumerable();

            if (isIEnumerable)
            {
                var argumentType = objectType.GetEnumerablArgumentType();

                if (typeof(T) == argumentType || typeof(T).IsSubclassOf(argumentType))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject(objectType);
            }
            else
            {
                var value = token.ToObject<T>();
                return JsonConvert.DeserializeObject($"[{JsonConvert.SerializeObject(value)}]", objectType, this);
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var items = (IEnumerable<T>)value;
            if (items.Count() == 1)
            {
                value = items.First();
            }

            unregisteredConvertersSerializer.Serialize(writer, value);
        }
    }
}
