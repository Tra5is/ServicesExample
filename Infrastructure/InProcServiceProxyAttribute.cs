using System;

namespace Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InProcServiceProxyAttribute : Attribute
    {
    }
}