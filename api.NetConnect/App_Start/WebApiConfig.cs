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
            config.Routes.MapHttpRoute(
               name: "POST_Auth_Logout",
               routeTemplate: "auth/logout",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Auth",
                   action = "Logout"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_Auth_CheckLogin",
               routeTemplate: "auth/check",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Auth",
                   action = "CheckLogin"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_Auth_Register",
               routeTemplate: "auth/register",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Auth",
                   action = "Register"
               }
            );
            #endregion


            #region ACCOUNT
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_Account_Reservations",
               routeTemplate: "account/reservation",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Account",
                   action = "Reservations"
               }
            );
            config.Routes.MapHttpRoute(
               name: "DELETE_Account_CancelReservation",
               routeTemplate: "account/reservation/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
               defaults: new
               {
                   controller = "Account",
                   action = "CancelReservation"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Account_Tournament",
               routeTemplate: "account/tournament",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Account",
                   action = "Tournaments"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Account_Edit",
               routeTemplate: "account/edit",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Account",
                   action = "Edit"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_Account_Edit",
               routeTemplate: "account/edit",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Account",
                   action = "Edit"
               }
            );
            #endregion
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
            #region Backend
            config.Routes.MapHttpRoute(
               name: "GET_User_Backend_Get",
               routeTemplate: "backend/user",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "User",
                   action = "Backend_Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "Put_User_Backend_FilterList",
               routeTemplate: "backend/user",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "User",
                   action = "Backend_FilterList"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_User_Backend_Detail",
               routeTemplate: "backend/user/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "User",
                   action = "Backend_Detail"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_User_Backend_Update",
               routeTemplate: "backend/user/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "User",
                   action = "Backend_Detail_Update"
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

            #region CATERING
            #region Frontend
            config.Routes.MapHttpRoute(
               name: "GET_Catering_GetProducts",
               routeTemplate: "catering/product",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Catering",
                   action = "Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Catering_GetProduct",
               routeTemplate: "catering/product/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Catering",
                   action = "Detail"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_Catering_Insert",
               routeTemplate: "order",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Catering",
                   action = "Insert"
               }
            );
            #endregion
            #region Backend
            config.Routes.MapHttpRoute(
               name: "GET_Catering_Backend_Get",
               routeTemplate: "backend/catering",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Catering",
                   action = "Backend_Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "Put_Catering_Backend_FilterList",
               routeTemplate: "backend/catering",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Catering",
                   action = "Backend_FilterList"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_Catering_Backend_Insert",
               routeTemplate: "backend/catering",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Catering",
                   action = "Backend_Detail_Insert"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Catering_Backend_New",
               routeTemplate: "backend/catering/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Catering",
                   action = "Backend_Detail_New"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Catering_Backend_Detail",
               routeTemplate: "backend/catering/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Catering",
                   action = "Backend_Detail"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_Catering_Backend_Update",
               routeTemplate: "backend/catering/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Catering",
                   action = "Backend_Detail_Update"
               }
            );
            #endregion
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
            #region Backend
            config.Routes.MapHttpRoute(
               name: "GET_Event_Backend_Get",
               routeTemplate: "backend/event",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Event",
                   action = "Backend_Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "Put_Event_Backend_FilterList",
               routeTemplate: "backend/event",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Event",
                   action = "Backend_FilterList"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Event_Backend_New",
               routeTemplate: "backend/event/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Event",
                   action = "Backend_Detail_New"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_Event_Backend_Insert",
               routeTemplate: "backend/event/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Event",
                   action = "Backend_Detail_Insert"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_Event_Backend_Detail",
               routeTemplate: "backend/event/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Event",
                   action = "Backend_Detail"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_Event_Backend_Update",
               routeTemplate: "backend/event/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Event",
                   action = "Backend_Detail_Update"
               }
            );
            config.Routes.MapHttpRoute(
               name: "DELETE_Event_Backend_Delete",
               routeTemplate: "backend/event",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
               defaults: new
               {
                   controller = "Event",
                   action = "Backend_Delete"
               }
            );
            #endregion
            #endregion

            #region EVENTTYPE
            #region Backend
            config.Routes.MapHttpRoute(
               name: "GET_EventType_Backend_Get",
               routeTemplate: "backend/eventtype",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "EventType",
                   action = "Backend_Get"
               }
            );
            config.Routes.MapHttpRoute(
               name: "Put_EventType_Backend_FilterList",
               routeTemplate: "backend/eventtype",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "EventType",
                   action = "Backend_FilterList"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_EventType_Backend_New",
               routeTemplate: "backend/eventtype/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "EventType",
                   action = "Backend_Detail_New"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_EventType_Backend_Insert",
               routeTemplate: "backend/eventtype/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "EventType",
                   action = "Backend_Detail_Insert"
               }
            );
            config.Routes.MapHttpRoute(
               name: "GET_EventType_Backend_Detail",
               routeTemplate: "backend/eventtype/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "EventType",
                   action = "Backend_Detail"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_EventType_Backend_Update",
               routeTemplate: "backend/eventtype/{id}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "EventType",
                   action = "Backend_Detail_Update"
               }
            );
            config.Routes.MapHttpRoute(
               name: "DELETE_EventType_Backend_Delete",
               routeTemplate: "backend/eventtype",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
               defaults: new
               {
                   controller = "EventType",
                   action = "Backend_Delete"
               }
            );
            #endregion
            #endregion

            #region GALLERY
            #region Frontend
            config.Routes.MapHttpRoute(
                name: "GET_Gallery_GetGallery",
                routeTemplate: "gallery",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                defaults: new
                {
                    controller = "Gallery",
                    action = "GetGallery"
                }
            );
            config.Routes.MapHttpRoute(
                name: "GET_Gallery_GetImages",
                routeTemplate: "gallery/{id}",
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                defaults: new
                {
                    controller = "Gallery",
                    action = "GetImages"
                }
            );
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
            config.Routes.MapHttpRoute(
               name: "POST_Seating_NewReservation",
               routeTemplate: "seating/{eventID}/{seatNumber}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Seating",
                   action = "NewReservation"
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
               routeTemplate: "tournament/{eventID}/{tournamentID}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Detail"
               }
            );
            config.Routes.MapHttpRoute(
               name: "POST_Tournament_Join",
               routeTemplate: "tournament/{eventID}/{tournamentID}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Join"
               }
            );
            config.Routes.MapHttpRoute(
               name: "PUT_Tournament_Join",
               routeTemplate: "tournament/{eventID}/{tournamentID}",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Leave"
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
               name: "POST_Tournament_Backend_Insert",
               routeTemplate: "backend/tournament/new",
               constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
               defaults: new
               {
                   controller = "Tournament",
                   action = "Backend_Detail_Insert"
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

            #region GAME
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
