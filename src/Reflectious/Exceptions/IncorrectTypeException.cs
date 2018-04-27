using System;

namespace Reflectious
{
    public class IncorrectTypeException : Exception
    {
    }
    
    public class IncorrectPropertyTypeException : IncorrectTypeException
    {
    }
    
    public class IncorrectInstanceTypeException : IncorrectTypeException
    {
    }
}