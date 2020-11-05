using System;
using System.Collections.Generic;
using System.Linq;
using JsonConverters.Collections.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConverters.Collections
{
    public class SingleOrArrayJsonConverter<T> : JsonConverter
    {
        /// <summary>
        /// Use a privately create serializer so we don't re-enter into CanConvert and cause a Newtonsoft exception
        /// </summary>
        private readonly JsonSerializer unregisteredConvertersSerializer = new JsonSerializer();

        public override bool CanRead => true;

        public override bool CanWrite => true;

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
