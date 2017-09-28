using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.Helper;

namespace api.NetConnect.Controllers
{
    public class InfoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            InfoViewModel viewmodel = new InfoViewModel();

            try
            {
                viewmodel.Data.FromModel(SettingsDataController.GetFirst());
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
