﻿using System;

namespace Firestorm
{
    internal interface IMethodFinder
    {
        Type[] GenericArguments { set; }
        Type[] ParameterTypes { set; }
        bool WantsParameterTypes { get; }

        IMethod Find();
    }

    internal interface ICacheableMethodFinder : IMethodFinder
    {
        new Type[] GenericArguments { get; set; }
        new Type[] ParameterTypes { get; set; }
        string GetCacheKey();
    }
}