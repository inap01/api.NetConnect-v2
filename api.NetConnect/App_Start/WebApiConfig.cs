using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Routing;

namespace api.NetConnect
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            config.Formatters.JsonFormatter.SerializerSettings.Culture = System.Globalization.CultureInfo.GetCultureInfo("de-DE");



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

            #region NAVIGATION
            config.Routes.MapHttpRoute(
               name: "GET_Navigation_Frontend",
               routeTemplate: "navigation",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Navigation",
                   action = "Frontend"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Navigation_Backend",
               routeTemplate: "backend/navigation",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Navigation",
                   action = "Backend"
               }
            );
            #endregion

            #region MEDIUM

            config.Routes.MapHttpRoute(
               name: "POST_Medium_Upload",
               routeTemplate: "medium",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Medium",
                   action = "Upload"
               }
            );

            #endregion

            #region AUTH
            config.Routes.MapHttpRoute(
               name: "POST_Auth_Auth",
               routeTemplate: "auth/login",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Auth",
                   action = "Auth"
               }
            );
            #endregion

            #region USER
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_User_Detail",
               routeTemplate: "user/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "User",
                   action = "Detail"
               }
            );
            #endregion
            #endregion

            #region NEWS
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_News_Get",
               routeTemplate: "news",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "News",
                   action = "Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_News_Detail",
               routeTemplate: "news/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "News",
                   action = "Detail"
               }
            );
            #endregion
            #region Backend
            config.Routes.MapHttpRoute(
               name: "GET_News_Backend_Get",
               routeTemplate: "backend/news",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "News",
                   action = "Backend_Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_News_Backend_Detail",
               routeTemplate: "backend/news/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "News",
                   action = "Backend_Detail"
               }
            );
            #endregion
            #endregion

            #region

            config.Routes.MapHttpRoute(
               name: "GET_Catering_GetProducts",
               routeTemplate: "catering/product",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Catering",
                   action = "GetProducts"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Catering_GetProduct",
               routeTemplate: "catering/product/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Catering",
                   action = "GetProduct"
               }
            );
            #endregion

            #region EVENT
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_Event_Get",
               routeTemplate: "event",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Event",
                   action = "Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Event_Detail",
               routeTemplate: "event/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Event",
                   action = "Detail"
               }
            );
            #endregion
            #endregion

            #region GALLERY
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_Gallery_Get",
               routeTemplate: "gallery",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Gallery",
                   action = "Get"
               }
            );
            #endregion

            #region GALLERYIMAGE
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_GalleryImage_Get",
               routeTemplate: "galleryimage/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "GalleryImage",
                   action = "Get"
               }
            );
            #endregion
            #endregion
            #endregion

            #region SEATING
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_Seating_Get",
               routeTemplate: "seating/{eventID}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Seating",
                   action = "Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Seating_Detail",
               routeTemplate: "seating/{eventID}/{seatNumber}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Seating",
                   action = "Detail"
               }
            );
            #endregion
            #endregion

            #region PARTNER
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_Partner_Get",
               routeTemplate: "partner",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Partner_Detail",
               routeTemplate: "partner/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Detail"
               }
            );
            #endregion
            #region Backend
            config.Routes.MapHttpRoute(
               name: "POST_Partner_Backend_Position",
               routeTemplate: "backend/partner/position",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Backend_Position"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_Partner_Backend_Position_Update",
               routeTemplate: "backend/partner/position",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Backend_Position_Update"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Partner_Backend_Get",
               routeTemplate: "backend/partner",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Backend_Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "Put_Partner_Backend_FilterList",
               routeTemplate: "backend/partner",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Backend_FilterList"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Partner_Backend_New",
               routeTemplate: "backend/partner/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Backend_Detail_New"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_Partner_Backend_Insert",
               routeTemplate: "backend/partner/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Backend_Detail_Insert"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Partner_Backend_Detail",
               routeTemplate: "backend/partner/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Backend_Detail"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_Partner_Backend_Update",
               routeTemplate: "backend/partner/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Backend_Detail_Update"
               }
            );
            config.Routes.MapHttpRoute(
               name: "DELETE_Partner_Backend_Delete",
               routeTemplate: "backend/partner",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
               defaults: new
               {
                   controller = "Partner",
                   action = "Backend_Delete"
               }
            );
            #endregion
            #endregion

            #region TOURNAMENT
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_Tournament_Get",
               routeTemplate: "tournament/{eventID}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Tournament_GetItem",
               routeTemplate: "tournament/{eventID}/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Detail"
               }
            );
            #endregion
            #region Backend
            config.Routes.MapHttpRoute(
               name: "GET_Tournament_Backend_Get",
               routeTemplate: "backend/tournament",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Backend_Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "Put_Tournament_Backend_FilterList",
               routeTemplate: "backend/tournament",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Backend_FilterList"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_Tournament_Backend_Insert",
               routeTemplate: "backend/tournament",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Backend_Detail_Insert"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Tournament_Backend_New",
               routeTemplate: "backend/tournament/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Backend_Detail_New"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Tournament_Backend_Detail",
               routeTemplate: "backend/tournament/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Backend_Detail"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_Tournament_Backend_Update",
               routeTemplate: "backend/tournament/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Backend_Detail_Update"
               }
            );
            config.Routes.MapHttpRoute(
               name: "DELETE_Tournament_Backend_Delete",
               routeTemplate: "backend/tournament",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Backend_Delete"
               }
            );
            #endregion
            #endregion

            #region TOURNAMENT
            #region Backend
            config.Routes.MapHttpRoute(
               name: "GET_Game_Backend_Get",
               routeTemplate: "backend/game",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Game",
                   action = "Backend_Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "Put_Game_Backend_FilterList",
               routeTemplate: "backend/game",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Game",
                   action = "Backend_FilterList"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_Game_Backend_Insert",
               routeTemplate: "backend/game",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Game",
                   action = "Backend_Detail_Insert"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Game_Backend_New",
               routeTemplate: "backend/game/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Game",
                   action = "Backend_Detail_New"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Game_Backend_Detail",
               routeTemplate: "backend/game/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Game",
                   action = "Backend_Detail"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_Game_Backend_Update",
               routeTemplate: "backend/game/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Game",
                   action = "Backend_Detail_Update"
               }
            );
            config.Routes.MapHttpRoute(
               name: "DELETE_Game_Backend_Delete",
               routeTemplate: "backend/tournament",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
               defaults: new
               {
                   controller = "Game",
                   action = "Backend_Delete"
               }
            );
            #endregion
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
