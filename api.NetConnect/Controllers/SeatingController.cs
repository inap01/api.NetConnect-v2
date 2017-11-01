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

namespace api.NetConnect.Controllers
{
    using SeatingListViewModel = ListViewModel<SeatingViewModelItem>;
    using BackendSeatingListViewModel = ListViewModel<BackendSeatingViewModelItem>;

    public class SeatingController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Get(Int32 eventID)
        {
            SeatingListViewModel viewmodel = new SeatingListViewModel();
            var seats = SeatDataController.GetByEvent(eventID);

            for (int i = 1; i <= 70; i++)
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

            try
            {
                viewmodel.Data.FromModel(SeatDataController.GetItem(seatNumber, eventID));
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
        public IHttpActionResult Backend_Get([FromUri] BackendSeatingFilter filter)
        {
            BackendSeatingListViewModel viewmodel = new BackendSeatingListViewModel();

            // TODO

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend_Detail(Int32 id)
        {
            BackendSeatingViewModel viewmodel = new BackendSeatingViewModel();

            // TODO

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
