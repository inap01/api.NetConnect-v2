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
    public class SeatingController : BaseController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get(Int32 eventID)
        {
            SeatingListViewModel viewmodel = new SeatingListViewModel();
            SeatDataController dataCtrl = new SeatDataController();

            var seats = dataCtrl.GetItems().Where(x => x.EventID == eventID);

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
            SeatDataController dataCtrl = new SeatDataController();

            try
            {
                viewmodel.BankAccount.FromProperties();
                viewmodel.Data.FromModel(dataCtrl.GetItem(seatNumber, eventID));

                if (viewmodel.Data.ReservationState < 0)
                    return Info(viewmodel, "Dieser Platz ist gesperrt und kann nicht reserviert werden.");
                else if(viewmodel.Data.ReservationState > 0)
                    return Warning(viewmodel, $"Dieser Platz wurde bereits von {viewmodel.Data.User.Nickname} reserviert.");
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult NewReservation(Int32 eventID, Int32 seatNumber)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            SeatDataController dataCtrl = new SeatDataController();

            try
            {
                SeatingViewModelItem seat = new SeatingViewModelItem().FromModel(dataCtrl.GetItem(seatNumber, eventID));
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
                    dataCtrl.Insert(item);
                }
                else if (seat.ReservationState < 0)
                {
                    return Warning(viewmodel, "Dieser Platz ist gesperrt und kann nicht reserviert werden.");
                }
                else
                {
                    return Warning(viewmodel, $"Dieser Platz wurde bereits von {seat.User.Nickname} reserviert.");
                }
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Platz wurde reserviert.");
        }
        #endregion
        #region Backend
        [HttpGet]
        public IHttpActionResult Backend_Get()
        {
            BackendSeatingListViewModel viewmodel = new BackendSeatingListViewModel();
            SeatDataController dataCtrl = new SeatDataController();
            EventDataController eventDataCtrl = new EventDataController();
            UserDataController userDataCtrl = new UserDataController();

            var events = eventDataCtrl.GetItems().OrderByDescending(x => x.Start).ToList();
            var users = userDataCtrl.GetItems().OrderBy(x => x.FirstName).ToList();
            var seats = dataCtrl.GetItems().Where(x => x.EventID == events[0].ID);

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
            SeatDataController dataCtrl = new SeatDataController();
            EventDataController eventDataCtrl = new EventDataController();
            UserDataController userDataCtrl = new UserDataController();

            var events = eventDataCtrl.GetItems().OrderByDescending(x => x.Start).ToList();
            var users = userDataCtrl.GetItems().OrderBy(x => x.FirstName).ToList();
            var seats = dataCtrl.GetItems().Where(x => x.EventID == filter.EventSelected.ID);

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
            SeatDataController dataCtrl = new SeatDataController();
            EventDataController eventDataCtrl = new EventDataController();
            UserDataController userDataCtrl = new UserDataController();

            try
            {
                viewmodel.UserOptions = userDataCtrl.GetItems().OrderBy(x => x.FirstName).ToList().ConvertAll(x =>
                {
                    return new BackendUserViewModelItem().FromModel(x);
                });

                var seats = dataCtrl.GetItems().Where(x => x.EventID == EventID);
                Seat model = seats.FirstOrDefault(x => x.SeatNumber == SeatNumber);
                if (model == null)
                    model = new Seat()
                    {
                        SeatNumber = SeatNumber,
                        State = 0,
                        Event = eventDataCtrl.GetItem(EventID)
                    };
                viewmodel.Data.FromModel(model);
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
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
