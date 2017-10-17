using api.NetConnect.data.ViewModel.Partner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    public class PartnerController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            PartnerViewModel model = new PartnerViewModel();
            model.Data.Display.Add("Header", false);
            model.Data.Display.Add("Footer", true);

            return Ok(model);
        }
    }
}
