using System;
using System.Collections.Generic;
using System.Text;

namespace ArgonCore.Interface
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class MapAttribute : ImplAttribute
    {
    }
}
