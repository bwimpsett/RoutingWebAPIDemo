using FirstWebAPIDemo.Models;
using System.Web.Http;
using System.Web.Http.Routing;

namespace FirstWebAPIDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("nonzero", typeof(NonZeroConstraint));
            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
