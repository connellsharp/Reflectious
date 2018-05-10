using System;

namespace Reflectious
{
    internal static class FinderUtility
    {
        private static readonly Cache<ulong, IMethod> Methods = new Cache<ulong, IMethod>();
        private static readonly Cache<ulong, IMethod> Constructors = new Cache<ulong, IMethod>();
        
        public static IMethodFinder GetMethodFinder(Type classType, string methodName, bool isStatic, Assume assume)
        {
            var finder = GetNonCachedFinder(classType, methodName, isStatic, assume);
            return new CachedMethodFinder(Methods, finder);
        }

        public static IMethodFinder GetConstructorFinder(Type classType, Assume assume)
        {
            var finder = new ConstructorFinder(classType);
            return new CachedMethodFinder(Constructors, finder);
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