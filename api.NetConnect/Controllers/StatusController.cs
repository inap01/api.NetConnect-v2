using api.NetConnect.data.ViewModel.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    public class StatusController : ApiController
    {
        public IHttpActionResult Get()
        {
            StatusViewModel model = null;

            Boolean isActive = Properties.Settings.Default.APIStatus_IsActive;
            String titel = Properties.Settings.Default.APIStatus_Titel;
            String text = Properties.Settings.Default.APIStatus_Text;
            model = isActive ? new StatusViewModel(null, null) : new StatusViewModel(titel, text);
            model.Success = Properties.Settings.Default.APIStatus_IsActive;

            return Ok(model);
        }
    }
}
