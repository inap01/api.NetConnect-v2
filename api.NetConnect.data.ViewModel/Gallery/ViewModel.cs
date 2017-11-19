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
        public virtual String Title { get; set; }
        public virtual Int32 ImageCount { get; set; }
        public virtual String Thumbnail { get; set; }
        public virtual Int32 EventID { get; set; }

        public GalleryViewModelItem()
        {

        }
    }

    public class GalleryImagesViewModel : BaseViewModel
    {
        public GalleryImageViewModelItem Data { get; set; }

        public GalleryImagesViewModel()
        {
            Data = new GalleryImageViewModelItem();
        }
    }
    public class GalleryImageViewModelItem : BaseViewModelItem
    {
        public String Title { get; set; }
        public Int32 ImageCount { get; set; }
        public String Thumbnail { get; set; }
        public Int32 EventID { get; set; }
        public String BaseURL { get; set; }
        
        public List<GalleryViewModelImageItem> Images { get; set; }
        public GalleryImageViewModelItem()
        {
            Images = new List<GalleryViewModelImageItem>();
        }
    }
    public class GalleryViewModelImageItem : BaseViewModelItem
    {
        public String RelativeURL { get; set; }
        public String RelativeURLThumbnail { get; set; }
        public Int32 EventID { get; set; }
    }

}