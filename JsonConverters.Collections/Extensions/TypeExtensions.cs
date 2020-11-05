using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonConverters.Collections.Extensions
{
    /// <summary>
    /// Extension methods that are to be used on <see cref="Type"/> objects.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Checks to see if a <see cref="Type"/> is an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that is to be inspected.</param>
        /// <returns>True if the specified <see cref="Type"/> is an <see cref="IEnumerable{T}"/>; Otherwise, false.</returns>
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

        /// <summary>
        /// Gets the argument type of an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to be inspected.</param>
        /// <returns>The <see cref="Type"/> of the generic argument for the given <see cref="IEnumerable{T}"/>.</returns>
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
