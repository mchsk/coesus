// plugin class that extends the attribute class  
using System;

namespace _interface
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class PluginAttribute : Attribute { }
}