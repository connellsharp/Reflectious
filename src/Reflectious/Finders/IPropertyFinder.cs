using System;

namespace Reflectious
{
    internal interface IPropertyFinder
    {
        IProperty Find();
        Type PropertyType { get; }
    }
}