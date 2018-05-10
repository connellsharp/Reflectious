using System;
using System.Collections.Generic;

namespace Reflectious
{
    internal class CacheKeyBuilder
    {
        /* 
            var builder = new StringBuilder(".ctor");
            builder.AppendFullTypeName(Type);
            builder.AppendFullTypeNames(GenericArguments);
            builder.AppendFullTypeNames(ParameterTypes); */
        
        public ulong Key { get; private set; }

        public void AddType(Type type, int shift = 0)
        {
            ulong hashCode = (ulong) type.GetHashCode();
            Key ^= hashCode << shift;
        }

        public void AddTypes(IEnumerable<Type> types, int startShift = 0)
        {
            if (types == null)
                return;
            
            foreach (Type type in types)
            {
                startShift++;
                
                if (startShift >= 8)
                    startShift = 0;
                
                AddType(type, startShift);
            }
        }

        public void AddString(string memberName, int shift = 8)
        {
            ulong hashCode = (ulong) memberName.GetHashCode();
            Key ^= hashCode << shift;
        }
    }
}