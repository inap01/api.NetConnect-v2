using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.data.ViewModel.ChangeSet;

namespace api.NetConnect.Controllers
{
    public class ChangeSetController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetItem(Int32 id)
        {
            ChangeSetViewModel viewmodel = new ChangeSetViewModel();

            try
            {
                viewmodel.Data.ID = 0;
                viewmodel.Data.LastChange = DateTime.Now;
                //viewmodel.Data.
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ex.Message);
            }

            return Ok(viewmodel);
        }
    }
}