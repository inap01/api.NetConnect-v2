using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.News
{
    public class FbNewsListViewModel : ListViewModel<FbNewsItem>
    {
        public FbNewsListViewModel() : base()
        {

        }
    }
    public class FbNews
    {
        public List<FbNewsItem> data { get; set; }
    }

    public class FbNewsItem
    {
        public String message { get; set; }
        public DateTime created_time { get; set; }
        public String story { get; set; }
        public String full_picture { get; set; }
        public String permalink_url { get; set; }
        public String id { get; set; }
        public FbLikes likes { get; set; }
        public FbComments comments { get; set; }
    }

    public class FbLikes
    {
        public List<FbUserItem> data { get; set; }
        public FbPaging paging { get; set; }
    }

    public class FbComments
    {
        public List<FbCommentItem> data { get; set; }
        public FbPaging paging { get; set; }

        public class FbCommentItem
        {
            public FbUserItem from { get; set; }
            public String message { get; set; }
            public String permalink_url { get; set; }
            public DateTime created_time { get; set; }
            public String id { get; set; }
        }
    }

    public class FbUserItem
    {
        public String id { get; set; }
        public String name { get; set; }
    }

public class FbPaging
    {
        public FbLikePagingCursor cursors { get; set; }

        public class FbLikePagingCursor
        {
            public String before { get; set; }
            public String after { get; set; }
        }
    }
}