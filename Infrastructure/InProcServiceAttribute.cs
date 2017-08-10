using System;

namespace Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InProcServiceAttribute : Attribute
    {
    }
}