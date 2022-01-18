using ApiRealState.GenerateToken.Business;
using Microsoft.AspNetCore.Cors;
using System.Web.Http.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiRealState
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            //only enable https port(443)
            config.Filters.Add(new RequireHttpsAttribute());

            //Only enable consumption in the same domain
            config.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*"));
        }
    }
}
