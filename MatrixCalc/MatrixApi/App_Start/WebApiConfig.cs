using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MatrixesDb;

namespace MatrixApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Clear(); 
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "Calculations",
               routeTemplate: "api/Calculations/{action}",
               defaults: new
               {
                   controller = "Calculations"
               }
             );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var builder = new ContainerBuilder();

            builder.RegisterType<MatrixRepository>().As<IMatrixRepository>().SingleInstance();

            builder.RegisterAssemblyTypes(
                    Assembly.GetExecutingAssembly())
                .Where(t =>
                    !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t))
                .InstancePerLifetimeScope();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
        }
    }
}
