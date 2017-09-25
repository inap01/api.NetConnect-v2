using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Routing;

namespace api.NetConnect
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            #region STATUS

            config.Routes.MapHttpRoute(
               name: "GET_Status_Get_Default",
               routeTemplate: "",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Status",
                   action = "Get"
               }
            );

            config.Routes.MapHttpRoute(
               name: "GET_Status_Get",
               routeTemplate: "status",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Status",
                   action = "Get"
               }
            );

            #endregion

            #region INFO

            config.Routes.MapHttpRoute(
               name: "GET_Info_Get",
               routeTemplate: "info",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Info",
                   action = "Get"
               }
            );

            #endregion

            #region PROFILE

            config.Routes.MapHttpRoute(
               name: "GET_Profile_GetItem",
               routeTemplate: "profile/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Profile",
                   action = "GetItem"
               }
            );

            #endregion

            #region TOURNAMENT

            config.Routes.MapHttpRoute(
               name: "GET_Tournament_GetItem",
               routeTemplate: "tournament/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "GetItem"
               }
            );

            #endregion

            #region CHANGESET

            config.Routes.MapHttpRoute(
               name: "GET_Changes_GetItem",
               routeTemplate: "Changes",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "ChangeSet",
                   action = "GetItem"
               }
            );

            #endregion
        }
    }
}
