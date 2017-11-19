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

namespace api.NetConnect.Controllers
{
    using GalleryListModel = ListViewModel<GalleryViewModelItem>;
    public class GalleryController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetImages(int id)
        {
            GalleryImagesViewModel viewmodel = new GalleryImagesViewModel();
            
            var items = GalleryImageDataController.GetItems(id);
            var ev = EventDataController.GetItem(id);

            viewmodel.Data.EventID = id;
            viewmodel.Data.ImageCount = items.Count;
            viewmodel.Data.Title = $"{ev.EventType.Name} Vol.{ev.Volume}";
            viewmodel.Data.Thumbnail = GalleryImageDataController.GetThumbnail(id).RelativeURL;

            foreach (var model in items)
            {
                viewmodel.Data.Images.Add(new GalleryViewModelImageItem().FromModel(model));
            }

            return Ok(viewmodel);
        }
        public IHttpActionResult GetGallery()
        {
            GalleryListModel viewmodel = new GalleryListModel();
            
            var ev = EventDataController.GetItems();


            foreach(var _event in ev)
            {
                var c = GalleryImageDataController.GetItems(_event.ID).Count;
                var eid = _event.ID;
                var thumb = GalleryImageDataController.GetThumbnail(_event.ID)?.RelativeURL;
                if (thumb == null || c == 0)
                    continue;
                var u = new GalleryViewModelItem()
                {
                    EventID = eid,
                    ImageCount = c,
                    Thumbnail = thumb,
                    Title = $"{_event.EventType.Name} Vol.{_event.Volume}",
                };
                viewmodel.Data.Add(u);
            }

            return Ok(viewmodel);
        }
    }
}
