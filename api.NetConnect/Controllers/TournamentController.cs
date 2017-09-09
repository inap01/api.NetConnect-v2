using api.NetConnect.ViewModels.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    public class TournamentController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(TournamentConverter.DataToViewModel());
        }
    }
}
