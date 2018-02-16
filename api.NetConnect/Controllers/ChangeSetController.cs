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
    public class ChangeSetController : BaseController
    {
        [HttpGet]
        public IHttpActionResult GetItem()
        {
            ChangeSetViewModel viewmodel = new ChangeSetViewModel();
            ChangeSetDataController dataCtrl = new ChangeSetDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(1));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }
    }
}