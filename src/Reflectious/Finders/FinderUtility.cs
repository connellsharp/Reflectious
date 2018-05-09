using System;

namespace Reflectious
{
    internal static class FinderUtility
    {
        private static readonly Cache<int, SingletonMethodFinder> MethodFinders = new Cache<int, SingletonMethodFinder>();
        
        public static IMethodFinder GetMethodFinder(Type classType, string methodName, bool isStatic, Assume assume)
        {
            IMethodFinder finder =
            MethodFinders.GetOrAdd(classType.GetHashCode() + methodName.GetHashCode(), 
                () => new SingletonMethodFinder(GetNonCachedFinder(classType, methodName, isStatic, assume)));

            return finder;
        }

        public static IMethodFinder GetConstructorFinder(Type classType, Assume assume)
        {
            IMethodFinder finder =
                MethodFinders.GetOrAdd(classType.GetHashCode(), 
                    () => new SingletonMethodFinder(new ConstructorFinder(classType)));

            return finder;
        }

        private static ICacheableMethodFinder GetNonCachedFinder(Type classType, string methodName, bool isStatic, Assume assume)
        {
            if (assume.HasFlag(Assume.UnambiguousName))
                return new SingleMethodFinder(classType, methodName, isStatic);
            
            return new MethodFinder(classType, methodName, isStatic);
        }

        public static IMethodFinder WrapForExtension(IMethodFinder finder, Type extensionThisParamType)
        {       
            return new ExtensionMethodFinder(finder, extensionThisParamType);
        }
    }
}