using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonConverters.Collections.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool IsIEnumerable(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var isIEnumerable = (type.IsInterface && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                || type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            return isIEnumerable;
        }

        internal static Type GetEnumerablArgumentType(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsIEnumerable())
            {
                throw new ArgumentException("The type specified must implement IEnumberable<>.", nameof(type));
            }

            if (type.IsGenericType)
            {
                return type.GetGenericArguments().First();
            }
            else
            {
                return type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)).GetGenericArguments().First();
            }
        }
    }
}
