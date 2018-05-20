using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Reflectious
{
    internal class ConstructorFinder : ICacheableMethodFinder
    {
        protected readonly Type Type;

        public ConstructorFinder(Type type)
        {
            Type = type;
        }

        public Type[] GenericArguments { get; set; }
        public Type[] ParameterTypes { get; set; }
        public bool WantsParameterTypes => ParameterTypes == null;

        public ulong GetCacheKey()
        {
            var keyBuilder = new CacheKeyBuilder();
            
            keyBuilder.AddType(Type);
            keyBuilder.AddTypes(GenericArguments);
            keyBuilder.AddTypes(ParameterTypes);

            return keyBuilder.Key;
        }

        public IMethod Find()
        {
            Type type = BuildFullType();
            
            //return new ActivatorConstructor(type);
            
            var ctorInfo = FindConstructorInfo(type);
            return new CompiledLambdaConstructor(ctorInfo);
            //return new ReflectionConstructor(ctorInfo);
        }

        private ConstructorInfo FindConstructorInfo(Type type)
        {
            List<ConstructorInfo> ctors = type.GetConstructors(Flags).ToList();

            switch (ctors.Count)
            {
                case 0:
                    throw new ConstructorNotFoundException("There are no public constructors.");

                case 1:
                    return ctors[0]; // Assume we use the only ctor
            }

            if(ParameterTypes == null)
                throw new ConstructorNotFoundException("Multiple constructors defined and no search criteria given.");

            if (ParameterTypes != null)
                ctors = ctors.Where(c => MatchUtilities.MatchesParameterTypes(c, ParameterTypes)).ToList();
                    
            switch (ctors.Count)
            {
                case 0:
                    throw new ConstructorNotFoundException("No constructors match the search criteria.");

                case 1:
                    return ctors[0];

                default:
                    var ctor = ctors.Find(c => c.GetParameters().Length == 0);
                    return ctor ?? throw new ConstructorNotFoundException("Multiple constructors match the search criteria.");
            }
        }

        private const BindingFlags Flags = BindingFlags.Public |
                                           BindingFlags.NonPublic |
                                           BindingFlags.Static |
                                           BindingFlags.Instance;

        private Type BuildFullType()
        {
            Type type = Type;

            if (GenericArguments != null)
                type = type.MakeGenericType(GenericArguments);
            
            return type;
        }
    }
}