using System;
using System.Collections.Generic;
using System.Globalization;
using JsonConverters.Collections.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConverters.Collections
{
    public class EnumerableJsonConverter : JsonConverter
    {
        public EnumerableJsonConverter(Type itemType)
        {
            ItemType = itemType;
        }

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public Type ItemType { get; }

        public override bool CanConvert(Type objectType)
        {
            var isIEnumerable = objectType.IsIEnumerable();

            if (isIEnumerable)
            {
                var argumentType = objectType.GetEnumerablArgumentType();

                if (ItemType == argumentType || ItemType.IsSubclassOf(argumentType))
                {
                    return true;
                }
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonArray = JArray.Load(reader);

            object deserializedList;
            if (objectType.IsInterface)
            {
                var argumentType = objectType.GetEnumerablArgumentType();

                var genericBase = typeof(List<>);
                var combinedType = genericBase.MakeGenericType(argumentType);

                deserializedList = Convert.ChangeType(Activator.CreateInstance(combinedType), combinedType, CultureInfo.InvariantCulture);
            }
            else if (objectType.IsArray)
            {
                var argumentType = objectType.GetEnumerablArgumentType();

                deserializedList = Activator.CreateInstance(objectType, jsonArray.Count);
                for (var i = 0; i < jsonArray.Count; i++)
                {
                    var typedValue = Convert.ChangeType(jsonArray[i], argumentType, CultureInfo.InvariantCulture);
                    ((Array)deserializedList).SetValue(typedValue, i);
                }

                return deserializedList;
            }
            else
            {
                deserializedList = Activator.CreateInstance(objectType);
            }

            serializer.Populate(jsonArray.CreateReader(), deserializedList);
            return deserializedList;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException($"{nameof(EnumerableJsonConverter)} only supports reading JSON.");
        }
    }
}
