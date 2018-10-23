using System;
using System.Reflection;

namespace Reflectious
{
    public static class MethodExtensions
    {   
        public static Type GetGenericArgument(this MethodBase method, int index = 0)
        {
            return method.GetGenericArguments()[index];
        }
    }
}