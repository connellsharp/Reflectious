using System;

namespace Reflectious
{
    public abstract class MemberNotFoundException : Exception
    {
        internal MemberNotFoundException(string message)
            : base(message)
        {
        }
    }
    
    public class MethodNotFoundException : MemberNotFoundException
    {
        public MethodNotFoundException(string methodName)
            : base("A method with the name '" + methodName + "' was not found.")
        {

        }
    }

    public class ConstructorNotFoundException : MemberNotFoundException
    {
        public ConstructorNotFoundException(string submessage)
            : base("A constructor could not be found: " + submessage)
        {

        }
    }

    public class PropertyNotFoundException : MemberNotFoundException
    {
        public PropertyNotFoundException(string propertyName)
            : base("A property with the name '" + propertyName + "' was not found.")
        {

        }
    }
}