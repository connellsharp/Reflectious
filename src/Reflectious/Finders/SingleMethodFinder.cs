using System;
using System.Reflection;
using System.Text;

namespace Reflectious
{
    internal class SingleMethodFinder : MemberFinder, ICacheableMethodFinder
    {
        public SingleMethodFinder(Type classType, string methodName, bool isStatic) 
            : base(classType, methodName, isStatic)
        {
        }

        public Type[] GenericArguments { get; set; }
        public Type[] ParameterTypes { get; set; }
        public bool WantsParameterTypes { get; } = false;

        public ulong GetCacheKey()
        {
            var keyBuilder = new CacheKeyBuilder();
            
            keyBuilder.AddString(MemberName);
            keyBuilder.AddTypes(GenericArguments);
            keyBuilder.AddTypes(ParameterTypes);

            return keyBuilder.Key;
        }

        public IMethod Find()
        {
            MethodInfo method = ClassType.GetMethod(MemberName, GetBindingFlags());
            return new ReflectionMethod(method);
        }
    }
}