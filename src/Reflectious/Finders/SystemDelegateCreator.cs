using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflectious
{
    /// <summary>
    /// Creates <see cref="Func{}"/> or <see cref="Action{}"/> delegate types from the given type arguments.
    /// </summary>
    internal class SystemDelegateCreator
    {
        private readonly Type _instanceType;
        private readonly Type[] _parameterTypes;
        private readonly Type _returnType;

        public SystemDelegateCreator(Type instanceType, Type[] parameterTypes, Type returnType)
        {
            _instanceType = instanceType;
            _parameterTypes = parameterTypes;
            _returnType = returnType != typeof(void) ? returnType : null;
        }

        public Type GetDelegateType()
        {
            Type[] types = GetGenericArgs().ToArray();

            if (_returnType != null)
                return GetFuncType(types.Length).MakeGenericType(types);
            else
                return GetActionType(types.Length).MakeGenericType(types);
        }

        private IEnumerable<Type> GetGenericArgs()
        {
            // open instance delegates have the first type as the instance type
            if (_instanceType != null)
                yield return _instanceType;

            if (_parameterTypes != null)
                foreach (var parameterType in _parameterTypes)
                    yield return parameterType;

            if (_returnType != null)
                yield return _returnType;
        }

        private static Type GetFuncType(int parameterCount)
        {
            switch (parameterCount)
            {
                case 1:
                    return typeof(Func<>);
                case 2:
                    return typeof(Func<,>);
                case 3:
                    return typeof(Func<,,>);
                case 4:
                    return typeof(Func<,,,>);
                case 5:
                    return typeof(Func<,,,,>);
                case 6:
                    return typeof(Func<,,,,,>);
                case 7:
                    return typeof(Func<,,,,,,>);
                case 8:
                    return typeof(Func<,,,,,,,>);
                default:
                    throw new NotSupportedException("More than 6 parameters are not supported.");
            }
        }

        private static Type GetActionType(int parameterCount)
        {
            switch (parameterCount)
            {
                case 1:
                    return typeof(Action<>);
                case 2:
                    return typeof(Action<,>);
                case 3:
                    return typeof(Action<,,>);
                case 4:
                    return typeof(Action<,,,>);
                case 5:
                    return typeof(Action<,,,,>);
                case 6:
                    return typeof(Action<,,,,,>);
                case 7:
                    return typeof(Action<,,,,,,>);
                default:
                    throw new NotSupportedException("More than 6 parameters are not supported.");
            }
        }
    }
}