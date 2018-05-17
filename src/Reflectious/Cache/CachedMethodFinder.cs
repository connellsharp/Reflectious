using System;

namespace Reflectious
{
    /// <summary>
    /// A <see cref="IMethodFinder"/> that stores a cache of the return values of <see cref="IMethodFinder.Find"/>
    /// based on a cache key defined by the setup of the properties.
    /// </summary>
    internal class CachedMethodFinder : IMethodFinder
    {
        private readonly Cache<ulong, IMethod> _cache;
        private readonly ICacheableMethodFinder _underlyingFinder;

        public CachedMethodFinder(Cache<ulong, IMethod> cache, ICacheableMethodFinder underlyingFinder)
        {
            _cache = cache;
            _underlyingFinder = underlyingFinder;
        }

        public Type[] GenericArguments
        {
            get { return _underlyingFinder.GenericArguments; }
            set { _underlyingFinder.GenericArguments = value; }
        }

        public Type[] ParameterTypes
        {
            get { return _underlyingFinder.ParameterTypes; }
            set { _underlyingFinder.ParameterTypes = value; }
        }

        public bool WantsParameterTypes
        {
            get { return _underlyingFinder.WantsParameterTypes; }
        }

        public IMethod Find()
        {
            return _cache.GetOrAdd(_underlyingFinder.GetCacheKey(), _underlyingFinder.Find);
        }
    }
}