using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Reflectious
{
    public static class CustomAttributeExensions
    {
        /// <summary>
        /// Checks whether a custom attribute of a specified type is applied to a specified member.
        /// </summary>
        [PublicAPI]
        public static bool HasCustomAttribute<T>(this MemberInfo memberInfo)
            where T : Attribute
        {
            return memberInfo.GetCustomAttribute<T>() != null;
        }
    }
}
