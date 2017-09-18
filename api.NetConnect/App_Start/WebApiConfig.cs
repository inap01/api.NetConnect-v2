using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace api.NetConnect
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste

            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            #region PROFILE

            config.Routes.MapHttpRoute(
               name: "GET_Profile_GetItem",
               routeTemplate: "profile/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new {
                   controller = "Profile",
                   action = "GetItem"
               }
            );

            #endregion
        }
    }
}
