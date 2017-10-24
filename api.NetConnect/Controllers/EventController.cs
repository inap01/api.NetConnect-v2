using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.Helper;
using api.NetConnect.data.ViewModel;

namespace api.NetConnect.Controllers
{
    using EventListViewModel = ListViewModel<EventViewModelItem>;
    //using BackendEventListViewModel = ListViewModel<BackendEventViewModelItem>;

    public class EventController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get()
        {
            EventListViewModel viewmodel = new EventListViewModel();

            try
            {
                viewmodel.Data.Add(new EventViewModelItem()
                {
                    ID = 1,
                    Title = "Playground Vol. 8",
                    Image = "http://lan-netconnect.de/_api/images/gallery/8/__preview.jpg",
                    Start = new DateTime(2017, 9, 8, 17, 0, 0),
                    End = new DateTime(2017, 9, 10, 12, 0, 0),
                    Description = "",
                    AddressLine1 = "Körrenzig",
                    AddressLine2 = "Hauptstraße 91",
                    AddressLine3 = "52441 Linnich",
                    RouteLink = "https://www.google.com/maps?ll=51.00048,6.282984&z=16&t=m&hl=de&gl=US&mapclient=embed&q=Hauptstra%C3%9Fe+93+52441+Linnich+Deutschland",
                    Price = 15
                });
                viewmodel.Data.Add(new EventViewModelItem()
                {
                    ID = 1,
                    Title = "Playground Vol. 9",
                    Image = "http://lan-netconnect.de/_api/images/gallery/8/__preview.jpg",
                    Start = new DateTime(2018, 3, 9, 17, 0, 0),
                    End = new DateTime(2018, 3, 11, 12, 0, 0),
                    Description = "",
                    AddressLine1 = "Körrenzig",
                    AddressLine2 = "Hauptstraße 91",
                    AddressLine3 = "52441 Linnich",
                    RouteLink = "https://www.google.com/maps?ll=51.00048,6.282984&z=16&t=m&hl=de&gl=US&mapclient=embed&q=Hauptstra%C3%9Fe+93+52441+Linnich+Deutschland",
                    Price = 15
                });
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 id)
        {
            EventViewModel viewmodel = new EventViewModel();

            try
            {
                viewmodel.Data = new EventViewModelItem()
                {
                    ID = 1,
                    Title = "Playground Vol. 8",
                    Image = "http://lan-netconnect.de/_api/images/gallery/8/__preview.jpg",
                    Start = new DateTime(2017, 9, 8, 17, 0, 0),
                    End = new DateTime(2017, 9, 10, 12, 0, 0),
                    Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. " +
                    "Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. " +
                    "At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.",
                    AddressLine1 = "Körrenzig",
                    AddressLine2 = "Hauptstraße 91",
                    AddressLine3 = "52441 Linnich",
                    RouteLink = "https://www.google.com/maps?ll=51.00048,6.282984&z=16&t=m&hl=de&gl=US&mapclient=embed&q=Hauptstra%C3%9Fe+93+52441+Linnich+Deutschland",
                    Price = 15
                };
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
        #endregion
    }
}
