using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Gallery
{
    public class GalleryViewModel : BaseViewModel
    {
        public GalleryViewModelItem Data { get; set; }

        public GalleryViewModel()
        {
            Data = new GalleryViewModelItem();
        }
    }

    public class GalleryViewModelItem : BaseViewModelItem
    {
        public String Title { get; set; }
        public Int32 ImageCount { get; set; }
        public String Thumbnail { get; set; }
        public Int32 EventID { get; set; }

        public GalleryViewModelItem()
        {

        }
    }
    public class GalleryViewModelImage : BaseViewModel
    {
        public GalleryViewModelImageItem Data { get; set; }

        public GalleryViewModelImage()
        {
            Data = new GalleryViewModelImageItem();
        }
    }
    public class GalleryViewModelImageItem : BaseViewModelItem
    {
        public String RelativeURL { get; set; }
        public String RelativeURLThumbnail { get; set; }
        public Int32 EventID { get; set; }
    }

}