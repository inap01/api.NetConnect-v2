using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.DataControllers;
using api.NetConnect.Converters;
using api.NetConnect.Helper;
using System.IO;

namespace api.NetConnect.Controllers
{
    public class GalleryController : BaseController
    {
        public IHttpActionResult GetGallery()
        {
            GalleryListModel viewmodel = new GalleryListModel();
            EventDataController dataCtrl = new EventDataController();

            var ev = dataCtrl.GetItems().OrderByDescending(x => x.Start);


            foreach (var _event in ev)
            {
                Int32 count;
                try
                {
                    count = GalleryDataController.Count(_event.ID);
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }

                var eid = _event.ID;
                var thumb = GalleryDataController.GetGalleryThumbnail(_event.ID)?.ImageUrl;
                if (thumb == null || count == 0)
                    continue;

                viewmodel.Data.Add(new GalleryViewModelListItem().FromModel(_event, count, thumb));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult GetImages(int id)
        {
            GalleryViewModel viewmodel = new GalleryViewModel();
            EventDataController dataCtrl = new EventDataController();

            var ev = dataCtrl.GetItem(id);
            var items = GalleryDataController.GetItems(id);

            viewmodel.FromModel(ev, items);

            return Ok(viewmodel);
        }
    }
}
