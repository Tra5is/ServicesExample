using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure;
using Owin;

namespace ClaimService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{Id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = new InProcServicesContainerBuilder()
                .WithHttpConfig(config)
                .RegisterController<ClaimController>()
                .Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);

            var service = container.Resolve<ClaimController>();
        }
    }
}
