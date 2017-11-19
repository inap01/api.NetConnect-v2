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
    using Converters;
    using DataControllers;
    using GalleryImageListViewModel = ListViewModel<GalleryViewModelImageItem>;

    public class GalleryImageController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetItems(int id)
        {
            GalleryImageListViewModel viewmodel = new GalleryImageListViewModel();

            var items = GalleryImageDataController.GetItems(id);

            foreach(var model in items)
            {
                GalleryViewModelImageItem item = new GalleryViewModelImageItem();
                item.FromModel(model);
                viewmodel.Data.Add(item);
            }

            return Ok(viewmodel);
        }
    }
}