﻿using System;

namespace Reflectious
{
    public static class ReflectorExtension
    {   
        public static StaticReflector Reflect(this Type type)
        {
            return new StaticReflector(type);
        }
        
        public static InstanceReflector<T> Reflect<T>(this T instance)
        {
            return new InstanceReflector<T>(new StrongInstanceGetter<T>(instance));
        }
        
    }
}