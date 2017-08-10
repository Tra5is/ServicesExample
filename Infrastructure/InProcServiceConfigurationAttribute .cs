using System;

namespace Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InProcServiceConfigurationAttribute : Attribute
    {
    }
}