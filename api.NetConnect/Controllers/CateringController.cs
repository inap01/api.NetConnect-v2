using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Catering;
using api.NetConnect.DataControllers;

namespace api.NetConnect.Controllers
{
    using api.NetConnect.Helper;
    using Converters;

    public class CateringController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            CateringViewModel viewmodel = new CateringViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            foreach (var model in CateringDataController.GetItems())
            {
                ProductViewModelItem item = new ProductViewModelItem();

                item.FromModel(model);
                viewmodel.Data.Add(item);
            }

            return Ok(viewmodel);
        }
    }
}