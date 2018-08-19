using System;

namespace Reflectious
{
    internal interface IMethodFinder
    {
        Type[] GenericArguments { get; set; }
        Type[] ParameterTypes { get; set; }
        bool WantsParameterTypes { get; }

        IMethod Find();
    }
}