using System;
using System.Linq;
using System.Reflection;

namespace ThoughtHaven
{
    public static class TestHelpers
    {
        public static TAttribute GetClassAttribute<TType, TAttribute>()
            where TAttribute : Attribute =>
            typeof(TType).GetTypeInfo().GetCustomAttribute<TAttribute>();

        public static TAttribute GetMethodAttribute<TType, TAttribute>(string methodName,
            int methodIndex = 0)
            where TAttribute : Attribute =>
            typeof(TType).GetMethods().Where(m => m.Name == methodName)
                .ToArray()[methodIndex].GetCustomAttribute<TAttribute>();

        public static TAttribute GetParameterAttribute<TType, TAttribute>(string methodName,
            string parameterName, int methodIndex = 0)
            where TAttribute : Attribute =>
            typeof(TType).GetMethods().Where(m => m.Name == methodName)
                .ToArray()[methodIndex].GetParameters().Where(p => p.Name == parameterName)
                .Single().GetCustomAttribute<TAttribute>();

        public static TAttribute GetPropertyAttribute<TType, TAttribute>(string propertyName)
            where TAttribute : Attribute =>
            typeof(TType).GetProperty(propertyName).GetCustomAttribute<TAttribute>();
    }
}