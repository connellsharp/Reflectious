namespace Reflectious
{
    public class ConstructorReflector<TType> : WeakMethodReflector<TType, TType>
        where TType : class
    {
        internal ConstructorReflector(IMethodFinder constructorFinder)
            : base(null, constructorFinder)
        {
        }
    }
}