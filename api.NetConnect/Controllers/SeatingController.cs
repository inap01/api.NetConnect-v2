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

namespace api.NetConnect.Controllers
{
    public class SeatingController : ApiController
    {
        [HttpPut]
        public IHttpActionResult FilterList()
        {
            return Ok();
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
                viewmodel.AddDangerAlert(ex.Message);
            }

            return Ok(viewmodel);
        }
    }
}
