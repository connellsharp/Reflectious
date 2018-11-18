using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reflectious
{
    public interface IMethod
    {
        object Invoke(object instance, object[] args);
        
        MethodInfo GetMethodInfo();
        
        IEnumerable<Type> GetParameterTypes();
    }
}