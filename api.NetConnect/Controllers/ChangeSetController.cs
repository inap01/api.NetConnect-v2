using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.data.ViewModel.ChangeSet;
using api.NetConnect.DataControllers;
using api.NetConnect.Helper;

namespace api.NetConnect.Controllers
{
    public class ChangeSetController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetItem()
        {
            ChangeSetViewModel viewmodel = new ChangeSetViewModel();

            try
            {
                viewmodel.Data.FromModel(ChangeSetDataController.GetItem(1));
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
    }
}