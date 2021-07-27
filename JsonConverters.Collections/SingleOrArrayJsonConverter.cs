﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JsonConverters.Collections.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConverters.Collections
{
    /// <summary>
    /// Provides serialization support for collections that are serialized as an array when there are multiple
    /// values, but when there is only one value only the value itself is serialized.
    /// </summary>
    public class SingleOrArrayJsonConverter : JsonConverter
    {
        private readonly Lazy<MethodInfo> castMethodLazy = new Lazy<MethodInfo>(() => typeof(Enumerable).GetMethod(nameof(Enumerable.Cast), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public));

        private readonly ConcurrentDictionary<Type, MethodInfo> genericCastMethodCache = new ConcurrentDictionary<Type, MethodInfo>();

        private readonly ConcurrentDictionary<Type, MethodInfo> genericToArrayMethodCache = new ConcurrentDictionary<Type, MethodInfo>();

        private readonly Lazy<MethodInfo> toArrayMethodLazy = new Lazy<MethodInfo>(() => typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public));

        /// <inheritdoc/>
        public override bool CanRead => true;

        /// <inheritdoc/>
        public override bool CanWrite => true;

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            var isIEnumerable = objectType.IsIEnumerable();

            return isIEnumerable;
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == null)
            {
                throw new ArgumentNullException(nameof(objectType));
            }

            System.Collections.IList results;

            var elementType = objectType.GetEnumerablArgumentType();

            if (objectType.IsArray || objectType.IsInterface)
            {
                var listType = typeof(List<>);
                var genericListType = listType.MakeGenericType(elementType);

                results = (System.Collections.IList)Activator.CreateInstance(genericListType);
            }
            else
            {
                results = (System.Collections.IList)Activator.CreateInstance(objectType);
            }

            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                var items = token.Select(x => x.ToObject(elementType));

                foreach (var item in items)
                {
                    results.Add(item);
                }
            }
            else
            {
                var value = token.ToObject(elementType);

                results.Add(value);
            }

            var argumentType = objectType.GetEnumerablArgumentType();
            if (argumentType != typeof(object))
            {
                var typedResults = Cast(results, argumentType);
                if (objectType.IsArray)
                {
                    var typedArray = ToArray(typedResults, argumentType);
                    return typedArray;
                }
                else
                {
                    return typedResults;
                }
            }

            if (objectType.IsArray)
            {
                var array = Array.CreateInstance(objectType.GetElementType(), results.Count);
                results.CopyTo(array, 0);
                return array;
            }
            else
            {
                return results;
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var items = (System.Collections.IEnumerable)value;
            var objectItems = items.Cast<object>();
            if (objectItems.Count() == 1)
            {
                var token = JToken.FromObject(objectItems.First());
                token.WriteTo(writer);
            }
            else
            {
                var array = new JArray(items);
                array.WriteTo(writer);
            }
        }

        private object Cast(object items, Type targetType)
        {
            var genericCastMethod = genericCastMethodCache.GetOrAdd(targetType, t =>
            {
                var castMethod = castMethodLazy.Value;
                var genericCastMethod = castMethod.MakeGenericMethod(targetType);
                return genericCastMethod;
            });

            var typedResults = genericCastMethod.Invoke(null, new[] { items });
            return typedResults;
        }

        private object ToArray(object items, Type targetType)
        {
            var genericToArrayMethod = genericToArrayMethodCache.GetOrAdd(targetType, t =>
            {
                var toArrayMethod = toArrayMethodLazy.Value;
                var genericToArrayMethod = toArrayMethod.MakeGenericMethod(targetType);
                return genericToArrayMethod;
            });

            var typedResults = genericToArrayMethod.Invoke(null, new[] { items });
            return typedResults;
        }
    }
}
