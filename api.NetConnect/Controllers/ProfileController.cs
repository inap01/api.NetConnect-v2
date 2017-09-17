using api.NetConnect.DataControllers;
using api.NetConnect.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    public class ProfileController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetItem(Int32 id)
        {


            ProfileViewModel viewmodel = new ProfileViewModel();

            try
            {
                var model = ProfileDataController.GetItem(id);
                viewmodel.Data = model.ToProfileViewModelItem();
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
