﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace RedirectApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "PostApi",
                routeTemplate: "api/{controller}/{action}", defaults: new { controller = "Values", action = "Post" }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{*catchcall}",
                defaults: new { controller = "Values", action = "Get" }
            );
        }
    }
}
