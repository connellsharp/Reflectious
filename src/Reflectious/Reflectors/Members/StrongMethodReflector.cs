using JetBrains.Annotations;

namespace Reflectious
{
    /// <summary>
    /// Exposes the fluent API for a method with one parameter of a type known at compile time.
    /// </summary>
    public class StrongMethodReflector<TInstance, TReturn, TArg1> : StrongMethodReflectorBase<TInstance, TReturn>
    {
        internal StrongMethodReflector(TInstance instance, [NotNull] IMethodFinder methodFinder) 
            : base(instance, methodFinder)
        {
        }

        [PublicAPI]
        public TReturn Invoke(TArg1 arg1)
        {
            return base.Invoke(arg1);
        }

        public StrongMethodReflector<TInstance, TNewReturn, TArg1> CastTo<TNewReturn>()
        {
            return new StrongMethodReflector<TInstance, TNewReturn, TArg1>(Instance, MethodFinder);
        }
    }
    
    /// <summary>
    /// Exposes the fluent API for a method with two parameters of types known at compile time.
    /// </summary>
    public class StrongMethodReflector<TInstance, TReturn, TArg1, TArg2> : StrongMethodReflectorBase<TInstance, TReturn>
    {
        internal StrongMethodReflector(TInstance instance, [NotNull] IMethodFinder methodFinder) 
            : base(instance, methodFinder)
        {
        }

        [PublicAPI]
        public TReturn Invoke(TArg1 arg1, TArg2 arg2)
        {
            return base.Invoke(arg1, arg2);
        }

        public StrongMethodReflector<TInstance, TNewReturn, TArg1, TArg2> CastTo<TNewReturn>()
        {
            return new StrongMethodReflector<TInstance, TNewReturn, TArg1, TArg2>(Instance, MethodFinder);
        }
    }
    
    /// <summary>
    /// Exposes the fluent API for a method with three parameters of types known at compile time.
    /// </summary>
    public class StrongMethodReflector<TInstance, TReturn, TArg1, TArg2, TArg3> : StrongMethodReflectorBase<TInstance, TReturn>
    {
        internal StrongMethodReflector(TInstance instance, [NotNull] IMethodFinder methodFinder) 
            : base(instance, methodFinder)
        {
        }

        [PublicAPI]
        public TReturn Invoke(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            return base.Invoke(arg1);
        }

        public StrongMethodReflector<TInstance, TNewReturn, TArg1, TArg2, TArg3> CastTo<TNewReturn>()
        {
            return new StrongMethodReflector<TInstance, TNewReturn, TArg1, TArg2, TArg3>(Instance, MethodFinder);
        }
    }
    
    /// <summary>
    /// Exposes the fluent API for a method with four parameters of types known at compile time.
    /// </summary>
    public class StrongMethodReflector<TInstance, TReturn, TArg1, TArg2, TArg3, TArg4> : StrongMethodReflectorBase<TInstance, TReturn>
    {
        internal StrongMethodReflector(TInstance instance, [NotNull] IMethodFinder methodFinder) 
            : base(instance, methodFinder)
        {
        }

        [PublicAPI]
        public TReturn Invoke(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            return base.Invoke(arg1);
        }

        public StrongMethodReflector<TInstance, TNewReturn, TArg1, TArg2, TArg3, TArg4> CastTo<TNewReturn>()
        {
            return new StrongMethodReflector<TInstance, TNewReturn, TArg1, TArg2, TArg3, TArg4>(Instance, MethodFinder);
        }
    }
}