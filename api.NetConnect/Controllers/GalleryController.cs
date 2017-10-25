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
    using GalleryListViewModel = ListViewModel<GalleryViewModelItem>;

    public class GalleryController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            GalleryListViewModel viewmodel = new GalleryListViewModel();

            for(int i = 0; i < 10; i++)
            {
                viewmodel.Data.Add(new GalleryViewModelItem()
                {
                    ID = (i+1),
                    Title = "Playground " + (i+1),
                    ImageCount = 100,
                    Thumbnail = "http://lan-netconnect.de/_api/images/gallery/8/__preview.jpg"
                });
            }

            return Ok(viewmodel);
        }
    }
}
