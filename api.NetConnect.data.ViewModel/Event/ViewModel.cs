using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Event
{
    public class EventViewModel : BaseViewModel
    {
        public EventViewModelItem Data { get; set; }

        public EventViewModel()
        {
            Data = new EventViewModelItem();
        }
    }

    public class EventViewModelItem : BaseViewModelItem
    {
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public String Image { get; set; }
        public String Text { get; set; }
        public String Link { get; set; }
        public List<FbLike> Likes { get; set; }
        public List<FbComment> Comments { get; set; }

        public EventViewModelItem()
        {
            Likes = new List<FbLike>();
            Comments = new List<FbComment>();
        }
    }

    public class FbLike
    {
        public String Name { get; set; }
        public String Image { get; set; }
        public String Link { get; set; }
    }

    public class FbComment : FbBaseComment
    {
        public List<FbBaseComment> Comments { get; set; }

        public FbComment()
        {
            Comments = new List<FbBaseComment>();
        }
    }

    public class FbBaseComment
    {
        public String Name { get; set; }
        public String Link { get; set; }
        public DateTime Date { get; set; }
        public String Text { get; set; }
    }
}