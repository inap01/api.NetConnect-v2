using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Seating;
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
    using SeatingListViewModel = ListArgsViewModel<SeatingViewModelItem, SeatingFilter, SeatingSortSettings>;
    using SeatingArgsRequest = ListArgsRequest<SeatingFilter, SeatingSortSettings>;

    public class SeatingController : ApiController
    {
        [HttpPut]
        public IHttpActionResult FilterList(SeatingArgsRequest args)
        {
            SeatingListViewModel viewmodel = new SeatingListViewModel();

            if (args == null)
                args = new SeatingArgsRequest();

            try
            {
                viewmodel = SeatingConverter.FilterList(args);
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
        public IHttpActionResult GetItem(Int32 id)
        {
            SeatingViewModel viewmodel = new SeatingViewModel();

            try
            {
                viewmodel.Data.FromModel(SeatDataController.GetItem(id));
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
    }
}
