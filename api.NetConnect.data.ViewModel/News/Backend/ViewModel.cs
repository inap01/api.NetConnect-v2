using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.News.Backend
{
    public class NewsViewModel : BackendBaseViewModel
    {
        public NewsViewModelItem Data { get; set; }

        public NewsViewModel()
        {
            Data = new NewsViewModelItem();
        }
    }

    public class NewsViewModelItem : BaseViewModelItem
    {
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public String Image { get; set; }
        public String Text { get; set; }
        public String Link { get; set; }
        public List<FbLike> Likes { get; set; }
        public List<FbComment> Comments { get; set; }

        public NewsViewModelItem()
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