using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Reflectious
{
    public static class CustomAttributeExtensions
    {
        /// <summary>
        /// Checks whether a custom attribute of a specified type is applied to a specified member.
        /// </summary>
        [PublicAPI]
        public static bool HasCustomAttribute<TAttribute>(this MemberInfo method)
            where TAttribute : Attribute
        {
            return method.GetCustomAttributes<TAttribute>().Any();
        }
        
        /// <summary>
        /// Checks whether a custom attribute of a specified type is applied to a specified member.
        /// </summary>
        [PublicAPI]
        public static bool HasCustomAttribute(this MemberInfo method, Type attributeType)
        {
            return method.GetCustomAttributes(attributeType).Any();
        }
    }
}
