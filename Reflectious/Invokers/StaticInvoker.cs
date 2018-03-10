﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Firestorm
{
    public class StaticInvoker : InstanceInvoker<object>
    {
        internal StaticInvoker(Type type)
            : base(type)
        {
        }

        [PublicAPI]
        public InstanceInvoker WithInstance(object instance)
        {
            return new InstanceInvoker(instance);
        }

        public MethodInvoker GetConstructor()
        {
            return new MethodInvoker(null, new ConstructorFinder(Type));
        }

        public StaticInvoker MakeGeneric(params Type[] types)
        {
            var genericType = Type.MakeGenericType(types);
            return new StaticInvoker(genericType);
        }
        
        [PublicAPI]
        public StaticInvoker MakeGeneric<T1>()
        {
            return MakeGeneric(typeof(T1));
        }

        [PublicAPI]
        public StaticInvoker MakeGeneric<T1, T2>()
        {
            return MakeGeneric(typeof(T1), typeof(T2));
        }

        [PublicAPI]
        public StaticInvoker MakeGeneric<T1, T2, T3>()
        {
            return MakeGeneric(typeof(T1), typeof(T2), typeof(T3));
        }

        [PublicAPI]
        public StaticInvoker MakeGeneric<T1, T2, T3, T4>()
        {
            return MakeGeneric(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        [PublicAPI]
        public StaticInvoker MakeGeneric<T1, T2, T3, T4, T5>()
        {
            return MakeGeneric(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        [PublicAPI]
        public StaticInvoker MakeGeneric<T1, T2, T3, T4, T5, T6>()
        {
            return MakeGeneric(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        [PublicAPI]
        public StaticInvoker MakeGeneric(IEnumerable<Type> types)
        {
            return MakeGeneric(types.ToArray());
        }
    }

    public class StaticInvoker<TType> : InstanceInvoker<TType>
        where TType : class
    {
        public StaticInvoker()
            : base(typeof(TType))
        {
        }

        [PublicAPI]
        public InstanceInvoker<TType> WithInstance(TType instance)
        {
            return new InstanceInvoker<TType>(instance);
        }
    }
}