using api.NetConnect.data;
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
    public class TestingController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetItem(Int32 id)
        {
            GenericDataController<User>.GetItem<String>("hartmann","LastName");
            
            return Ok();
        }
    }
}
