using System;
using System.Collections.Generic;
using System.Globalization;
using JsonConverters.Collections.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConverters.Collections
{
    /// <summary>
    /// Provides deserialization support of serialized arrays to any type that implements <see cref="IEnumerable{T}"/>.
    /// </summary>
    public class EnumerableJsonConverter : JsonConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableJsonConverter"/> class.
        /// </summary>
        /// <param name="itemType">The <see cref="Type"/> of the objects stored in the collection.</param>
        public EnumerableJsonConverter(Type itemType)
        {
            ItemType = itemType;
        }

        /// <inheritdoc/>
        public override bool CanRead => true;

        /// <inheritdoc/>
        public override bool CanWrite => false;

        /// <summary>
        /// Gets the <see cref="Type"/> of objects stored in the collections to be deserialized.
        /// </summary>
        public Type ItemType { get; }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

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

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException($"{nameof(EnumerableJsonConverter)} only supports reading JSON.");
        }
    }
}
