using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Gallery
{
    public class GalleryListModel : ListViewModel<GalleryViewModelListItem>
    {
        public GalleryListModel() : base()
        {

        }
    }
    
    public class GalleryViewModelListItem : BaseViewModelItem
    {
        public virtual String Title { get; set; }
        public virtual Int32 ImageCount { get; set; }
        public virtual String Thumbnail { get; set; }

        public GalleryViewModelListItem()
        {

        }
    }

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
        public String BaseURL { get; set; }
        
        public List<GalleryViewModelImageItem> Images { get; set; }
        public GalleryViewModelItem()
        {
            Images = new List<GalleryViewModelImageItem>();
        }
    }
    public class GalleryViewModelImageItem
    {
        public String ImageUrl { get; set; }
        public String ThumbnailUrl { get; set; }
    }
}