using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    using DataControllers;
    using GalleryListViewModel = ListViewModel<GalleryViewModelItem>;

    public class GalleryController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            GalleryListViewModel viewmodel = new GalleryListViewModel();

            GalleryImageDataController.GetItems(8);

            return Ok(viewmodel);
        }
    }
}
