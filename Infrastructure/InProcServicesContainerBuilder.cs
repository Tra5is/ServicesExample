using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace Infrastructure
{
    public class InProcServicesContainerBuilder
    {
        private Autofac.ContainerBuilder containerBuilder;

        public InProcServicesContainerBuilder()
        {
            this.containerBuilder = new Autofac.ContainerBuilder();
        }

        public IContainer Build()
        {
            RegisterInProcServiceConfigurations();
            RegisterInProcServices();
            RegisterInProcServiceProxies();
            RegisterApiControllers();

            return this.containerBuilder.Build();
        }

        public InProcServicesContainerBuilder WithHttpConfig(HttpConfiguration config)
        {
            containerBuilder.RegisterWebApiFilterProvider(config);
            return this;
        }

        public InProcServicesContainerBuilder RegisterController<T>()
            where T : ApiController
        {
            containerBuilder.RegisterType<T>();
            return this;
        }

        private void RegisterInProcServiceConfigurations()
        {
            containerBuilder.RegisterTypes(GetTypesWithAttribute<InProcServiceConfigurationAttribute>(true).ToArray())
                .SingleInstance();
        }

        private void RegisterApiControllers()
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        private void RegisterInProcServices()
        {
            containerBuilder.RegisterTypes(GetTypesWithAttribute<InProcServiceAttribute>(true).ToArray())
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private void RegisterInProcServiceProxies()
        {
            containerBuilder.RegisterTypes(GetTypesWithAttribute<InProcServiceProxyAttribute>(true).ToArray())
                .AsImplementedInterfaces();
        }

        private IEnumerable<Type> GetTypesWithAttribute<TAttribute>(bool inherit)
            where TAttribute : Attribute
        {
            var  types = from a in LoadAllBinDirectoryAssemblies()
                from t in a.GetTypes()
                where t.GetCustomAttributes(inherit).Any(attr => attr.GetType() == typeof(TAttribute))
                select t;

            return types;
        }

        private IEnumerable<Assembly> LoadAllBinDirectoryAssemblies()
        {
            string binPath = GetCurrentAssemblyLocation();

            foreach (string dll in Directory.GetFiles(binPath, "*.dll", SearchOption.AllDirectories))
            {
                Assembly loadedAssembly = null;
                try
                {
                    loadedAssembly = Assembly.LoadFile(dll);
                }
                catch (FileLoadException)
                { } // The Assembly has already been loaded.
                catch (BadImageFormatException)
                { } // If a BadImageFormatException exception is thrown, the file is not an assembly.

                if (null != loadedAssembly)
                    yield return loadedAssembly;
            }
        }

        private string GetCurrentAssemblyLocation()
        {

            string binPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return (Directory.Exists(binPath))
                ? binPath
                : currentPath;
        }
    }
}