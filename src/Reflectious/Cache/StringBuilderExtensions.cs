using System;
using System.Text;

namespace Reflectious
{
    internal static class StringBuilderExtensions
    {
        internal static void AppendFullTypeName(this StringBuilder builder, Type genericArgument)
        {
            builder.Append(genericArgument.GetHashCode());
        }

        internal static void AppendFullTypeNames(this StringBuilder builder, Type[] genericArguments)
        {
            if (genericArguments != null)
                foreach (var type in genericArguments)
                    builder.Append(type.GetHashCode());
        }
    }
}