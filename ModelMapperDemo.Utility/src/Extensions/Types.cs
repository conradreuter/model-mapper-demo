using System;
using System.Runtime.CompilerServices;

namespace ModelMapperDemo.Utility.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Run the static initializer of a type.
        /// Forces the initialization of all static fields and properties of the type.
        /// </summary>
        public static void RunStaticInitializer(this Type type)
        {
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }
    }
}
