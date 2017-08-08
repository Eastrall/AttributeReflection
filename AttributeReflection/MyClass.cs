using System;

namespace AttributeReflection
{
    public class MyClass
    {
        [ReflectedMethod("Hello world!")]
        public void DisplayData(string data)
        {
            Console.WriteLine(data);
        }

        [ReflectedMethod("Hello world! 2")]
        public void DisplayData2(string data)
        {
            Console.WriteLine(data);
        }

        [ReflectedMethod("Hello world! 3")]
        public void DisplayData3(string data)
        {
            Console.WriteLine(data);
        }

        [ReflectedMethod("Hello world! 4")]
        public void DisplayData4(string data)
        {
            Console.WriteLine(data);
        }
    }
}