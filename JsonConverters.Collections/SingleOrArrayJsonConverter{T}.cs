using System;
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
    /// <typeparam name="T">The type of values stored in the collection.</typeparam>
    public class SingleOrArrayJsonConverter<T> : SingleOrArrayJsonConverter
    {
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
    }
}
