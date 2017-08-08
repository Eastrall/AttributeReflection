using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AttributeReflection
{
    class Program
    {
        private static readonly Dictionary<string, MethodInfo> _methodsInfo = new Dictionary<string, MethodInfo>();
        private static readonly Dictionary<string, Action<MyClass, string>> _methodsActions = new Dictionary<string, Action<MyClass, string>>();

        static void Main(string[] args)
        {
            LoadFromReflection();
            DisplayReflectionMethods();
            DisplayReflectionActions();
            Console.ReadLine();
        }

        private static void LoadFromReflection()
        {
            var type = typeof(MyClass);
            var typeInfo = type.GetTypeInfo();
            var methodsInfo = typeInfo.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            var methodsWithAttribute = from x in methodsInfo
                                       where x.GetCustomAttribute<ReflectedMethodAttribute>() != null
                                       select x;

            if (!methodsWithAttribute.Any())
            {
                Console.WriteLine("No methods with attribute: {0}", nameof(ReflectedMethodAttribute));
                return;
            }

            foreach (var methodWithAttribute in methodsWithAttribute)
            {
                var attribute = methodWithAttribute.GetCustomAttribute<ReflectedMethodAttribute>();

                if (attribute == null)
                    continue;

                _methodsInfo.Add(attribute.Data, methodWithAttribute);

                var action = methodWithAttribute.CreateDelegate(typeof(Action<MyClass, string>)) as Action<MyClass, string>;

                _methodsActions.Add(attribute.Data, action);
            }
        }

        private static void DisplayReflectionMethods()
        {
            long totalTicks = 0;
            var myClass = new MyClass();
            var watch = new Stopwatch();

            Console.WriteLine("========= METHODS INFO REFLECTION ==========");
            foreach (var method in _methodsInfo)
            {
                watch.Start();
                method.Value.Invoke(myClass, new object[] { method.Key });
                watch.Stop();
                Console.WriteLine("-> Invoked in {0} ticks", watch.ElapsedTicks);
                totalTicks += watch.ElapsedTicks;
            }
            Console.WriteLine("TOTAL TICKS FOR METHOD INFO: {0}", totalTicks);
        }

        private static void DisplayReflectionActions()
        {
            long totalTicks = 0;
            var myClass = new MyClass();
            var watch = new Stopwatch();

            Console.WriteLine("========= METHODS ACTIONS REFLECTION ==========");
            foreach (var method in _methodsActions)
            {
                watch.Start();
                method.Value.Invoke(myClass, method.Key);
                watch.Stop();
                Console.WriteLine("-> Invoked in {0} ticks", watch.ElapsedTicks);
                totalTicks += watch.ElapsedTicks;
            }
            Console.WriteLine("TOTAL TICKS FOR METHOD ACTION: {0}", totalTicks);
        }
    }
}