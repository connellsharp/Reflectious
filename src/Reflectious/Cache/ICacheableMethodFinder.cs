namespace Reflectious
{
    internal interface ICacheableMethodFinder : IMethodFinder
    {
        ulong GetCacheKey();
    }
}