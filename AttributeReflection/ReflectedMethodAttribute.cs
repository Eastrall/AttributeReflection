using System;

namespace AttributeReflection
{
    public class ReflectedMethodAttribute : Attribute
    {
        public string Data { get; set; }

        public ReflectedMethodAttribute(string data)
        {
            this.Data = data;
        }
    }
}