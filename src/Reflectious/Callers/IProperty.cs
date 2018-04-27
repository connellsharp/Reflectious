using System;

namespace Reflectious
{
    public interface IProperty
    {
        Type PropertyTyoe { get; }
        
        object GetValue(object instance);
        
        void SetValue(object instance, object value);
    }
}