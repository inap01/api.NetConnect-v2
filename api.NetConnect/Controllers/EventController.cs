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
using api.NetConnect.data.ViewModel.Event.Backend;
using api.NetConnect.data.ViewModel.EventType.Backend;

namespace api.NetConnect.Controllers
{
    using EventListViewModel = ListViewModel<EventViewModelItem>;
    using BackendEventListArgs = ListArgsRequest<BackendEventFilter>;
    using BackendEventListViewModel = ListArgsViewModel<BackendEventViewModelItem, BackendEventFilter>;

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
                    ID = 9,
                    Title = "NetConnect & Friends Vol. 1",
                    Image = "http://lan-netconnect.de/_api/images/gallery/8/__preview.jpg",
                    Start = new DateTime(2017, 9, 8, 17, 0, 0),
                    End = new DateTime(2017, 9, 10, 12, 0, 0),
                    Description = "",
                    District = "Körrenzig",
                    Street = "Hauptstraße",
                    Housenumber = "91",
                    Postcode = "52441",
                    City = "Linnich",
                    RouteLink = "https://www.google.com/maps?ll=51.00048,6.282984&z=16&t=m&hl=de&gl=US&mapclient=embed&q=Hauptstra%C3%9Fe+93+52441+Linnich+Deutschland",
                    Price = 15,
                    Seating = new EventViewModelItem.SeatingReservation()
                    {
                        SeatsCount = 40,
                        Free = 15,
                        Flagged = 5,
                        Reserved = 20
                    }
                });
                viewmodel.Data.Add(new EventViewModelItem()
                {
                    ID = 10,
                    Title = "Playground Vol. 9",
                    Image = "http://lan-netconnect.de/_api/images/gallery/8/__preview.jpg",
                    Start = new DateTime(2018, 3, 9, 17, 0, 0),
                    End = new DateTime(2018, 3, 11, 12, 0, 0),
                    Description = "",
                    District = "Körrenzig",
                    Street = "Hauptstraße",
                    Housenumber = "91",
                    Postcode = "52441",
                    City = "Linnich",
                    RouteLink = "https://www.google.com/maps?ll=51.00048,6.282984&z=16&t=m&hl=de&gl=US&mapclient=embed&q=Hauptstra%C3%9Fe+93+52441+Linnich+Deutschland",
                    Price = 15,
                    Seating = new EventViewModelItem.SeatingReservation()
                    {
                        SeatsCount = 70,
                        Free = 30,
                        Reserved = 40
                    }
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
                    ID = id,
                    Title = "Playground Vol. 8",
                    Image = "http://lan-netconnect.de/_api/images/gallery/8/__preview.jpg",
                    Start = new DateTime(2017, 9, 8, 17, 0, 0),
                    End = new DateTime(2017, 9, 10, 12, 0, 0),
                    Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. " +
                    "Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. " +
                    "At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.",
                    District = "Körrenzig",
                    Street = "Hauptstraße",
                    Housenumber = "91",
                    Postcode = "52441",
                    City = "Linnich",
                    RouteLink = "https://www.google.com/maps?ll=51.00048,6.282984&z=16&t=m&hl=de&gl=US&mapclient=embed&q=Hauptstra%C3%9Fe+93+52441+Linnich+Deutschland",
                    Price = 15,
                    Seating = new EventViewModelItem.SeatingReservation()
                    {
                        SeatsCount = 70,
                        Free = 30,
                        Reserved = 40
                    }
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

        #region Backend
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendEventListViewModel viewmodel = new BackendEventListViewModel();
            BackendEventListArgs args = new BackendEventListArgs();

            try
            {
                Int32 TotalItemsCount;
                viewmodel.Data = EventConverter.FilterList(args, out TotalItemsCount);

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendEventListArgs args)
        {
            BackendEventListViewModel viewmodel = new BackendEventListViewModel();

            try
            {
                viewmodel.Filter.Name = args.Filter.Name;
                viewmodel.Pagination = args.Pagination;

                Int32 TotalItemsCount;
                viewmodel.Data = EventConverter.FilterList(args, out TotalItemsCount);

                viewmodel.Pagination.TotalItemsCount = TotalItemsCount;
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendEventViewModel viewmodel = new BackendEventViewModel();

            try
            {
                viewmodel.EventTypeOptions = EventTypeDataController.GetItems().ConvertAll(x =>
                {
                    return new BackendEventTypeViewModelItem() { ID = x.ID, Name = x.Name };
                }).OrderBy(x => x.Name).ToList();

                viewmodel.Data.FromModel(EventDataController.GetItem(id));
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail_New()
        {
            BackendEventViewModel viewmodel = new BackendEventViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendEventViewModelItem request)
        {
            BackendEventViewModel viewmodel = new BackendEventViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendEventViewModelItem request)
        {
            BackendEventViewModel viewmodel = new BackendEventViewModel();

            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler ist aufgetreten:");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendEventDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            // TODO

            return Ok(viewmodel);
        }
        #endregion
    }
}
