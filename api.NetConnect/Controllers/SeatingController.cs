using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Seating;
using api.NetConnect.data.ViewModel.Seating.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.data.ViewModel;
using api.NetConnect.Helper;
using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel.User.Backend;
using api.NetConnect.data.ViewModel.Event.Backend;

namespace api.NetConnect.Controllers
{
    public class SeatingController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get(Int32 eventID)
        {
            SeatingListViewModel viewmodel = new SeatingListViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;
            var seats = SeatDataController.GetByEvent(eventID);

            for (int i = 1; i <= Properties.Settings.Default.SeatAmount; i++)
            {
                SeatingViewModelItem item = new SeatingViewModelItem();
                Seat model = seats.FirstOrDefault(x => x.SeatNumber == i);
                if (model == null)
                    model = new Seat()
                    {
                        SeatNumber = i,
                        State = 0
                    };
                item.FromModel(model);
                viewmodel.Data.Add(item);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Detail(Int32 eventID, Int32 seatNumber)
        {
            SeatingViewModel viewmodel = new SeatingViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                viewmodel.BankAccount.FromProperties();
                viewmodel.Data.FromModel(SeatDataController.GetItem(seatNumber, eventID));

                if (viewmodel.Data.ReservationState < 0)
                    viewmodel.AddInfoAlert("Dieser Platz ist gesperrt und kann nicht reserviert werden.");
                else if(viewmodel.Data.ReservationState > 0)
                    viewmodel.AddWarningAlert($"Dieser Platz wurde bereits von {viewmodel.Data.User.Nickname} reserviert.");
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult NewReservation(Int32 eventID, Int32 seatNumber)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                SeatingViewModelItem seat = new SeatingViewModelItem().FromModel(SeatDataController.GetItem(seatNumber, eventID));
                if (seat.ReservationState == 0)
                {
                    Seat item = new Seat()
                    {
                        EventID = eventID,
                        ReservationDate = DateTime.Now,
                        UserID = UserHelper.CurrentUserID,
                        SeatNumber = seatNumber,
                        IsActive = true,
                        Payed = false,
                        State = 1,
                        Description = ""
                    };
                    SeatDataController.Insert(item);

                    viewmodel.AddSuccessAlert("Platz wurde reserviert.");
                }
                else if (seat.ReservationState < 0)
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Dieser Platz ist gesperrt und kann nicht reserviert werden.");
                }
                else
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert($"Dieser Platz wurde bereits von {seat.User.Nickname} reserviert.");
                }
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
            BackendSeatingListViewModel viewmodel = new BackendSeatingListViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            var events = EventDataController.GetItems().OrderByDescending(x => x.Start).ToList();
            var users = UserDataController.GetItems().OrderBy(x => x.FirstName).ToList();
            var seats = SeatDataController.GetByEvent(events[0].ID);

            viewmodel.Filter.EventOptions = events.ConvertAll(x =>
            {
                return new SeatingFilterEvent()
                {
                    ID = x.ID,
                    Name = $"{x.EventType.Name} Vol.{x.Volume}"
                };
            });
            viewmodel.Filter.EventSelected = viewmodel.Filter.EventOptions[0];

            for (int i = 1; i <= Properties.Settings.Default.SeatAmount; i++)
            {
                BackendSeatingViewModelItem item = new BackendSeatingViewModelItem();
                Seat model = seats.FirstOrDefault(x => x.SeatNumber == i);
                if (model == null)
                    model = new Seat()
                    {
                        SeatNumber = i,
                        State = 0,
                        Event = events[0]
                    };
                item.FromModel(model);
                viewmodel.Data.Add(item);
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_FilterList(BackendSeatingFilter filter)
        {
            BackendSeatingListViewModel viewmodel = new BackendSeatingListViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            var events = EventDataController.GetItems().OrderByDescending(x => x.Start).ToList();
            var users = UserDataController.GetItems().OrderBy(x => x.FirstName).ToList();
            var seats = SeatDataController.GetByEvent(filter.EventSelected.ID);

            viewmodel.Filter.EventOptions = events.ConvertAll(x =>
            {
                return new SeatingFilterEvent()
                {
                    ID = x.ID,
                    Name = $"{x.EventType.Name} Vol.{x.Volume}"
                };
            });

            for (int i = 1; i <= Properties.Settings.Default.SeatAmount; i++)
            {
                BackendSeatingViewModelItem item = new BackendSeatingViewModelItem();
                Seat model = seats.FirstOrDefault(x => x.SeatNumber == i);
                if (model == null)
                    model = new Seat()
                    {
                        SeatNumber = i,
                        State = 0,
                        Event = events[0]
                    };
                item.FromModel(model);
                viewmodel.Data.Add(item);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 EventID, Int32 SeatNumber)
        {
            BackendSeatingViewModel viewmodel = new BackendSeatingViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                viewmodel.UserOptions = UserDataController.GetItems().OrderBy(x => x.FirstName).ToList().ConvertAll(x =>
                {
                    return new BackendUserViewModelItem().FromModel(x);
                });

                var seats = SeatDataController.GetByEvent(EventID);
                Seat model = seats.FirstOrDefault(x => x.SeatNumber == SeatNumber);
                if (model == null)
                    model = new Seat()
                    {
                        SeatNumber = SeatNumber,
                        State = 0,
                        Event = EventDataController.GetItem(EventID)
                    };
                viewmodel.Data.FromModel(model);
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Backend_Detail_Insert(BackendSeatingViewModelItem request)
        {
            BackendSeatingViewModel viewmodel = new BackendSeatingViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Backend_Detail_Update(Int32 id, BackendSeatingViewModelItem request)
        {
            BackendSeatingViewModel viewmodel = new BackendSeatingViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpDelete]
        public IHttpActionResult Backend_Delete(BackendSeatingDeleteRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            return Ok(viewmodel);
        }
        #endregion
    }
}
