using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Util
{
    public class Types
    {
        public static IEnumerable<Type> FindSubClassesOf<TBaseType>()
        {
            var base_type = typeof(TBaseType);
            var assembly = base_type.Assembly;

            return assembly.GetTypes().Where(t => t.IsSubclassOf(base_type));
        }

        public static IEnumerable<Type> FindSubClassesOfGeneric(Type t, System.Reflection.Assembly assembly = null)
        {
            if (assembly == null) assembly = t.Assembly;

            return assembly.GetTypes().Where(x =>
                            x.BaseType != null &&
                            x.BaseType.IsGenericType &&
                            x.BaseType.GetGenericTypeDefinition() == t);
        }
    }
}
