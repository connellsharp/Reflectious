using System;
using JetBrains.Annotations;

namespace Reflectious
{
    public abstract class StrongMethodReflectorBase<TInstance, TReturn> : MethodReflectorBase<TInstance, TReturn>
    {
        internal StrongMethodReflectorBase(TInstance instance, [NotNull] IMethodFinder methodFinder)
            : base(instance, methodFinder)
        {
        }
    }
}