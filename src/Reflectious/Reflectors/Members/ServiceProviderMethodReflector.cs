using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Reflectious
{
    public class ServiceProviderMethodReflector<TInstance, TReturn>: MethodReflectorBase<TInstance, TReturn>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<Type> _paramTypes;

        internal ServiceProviderMethodReflector(TInstance instance, [NotNull] IMethodFinder methodFinder, 
            IServiceProvider serviceProvider) 
            : base(instance, methodFinder)
        {
            _paramTypes = methodFinder.ParameterTypes
                          ?? methodFinder.Find().GetParameterTypes();

            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Invokes the method using parameter values from the <see cref="IServiceProvider"/>.
        /// </summary>
        [PublicAPI]
        public TReturn Invoke()
        {
            object[] args = _paramTypes.Select(t => _serviceProvider.GetService(t)).ToArray();
            return base.Invoke(args);
        }
    }
}