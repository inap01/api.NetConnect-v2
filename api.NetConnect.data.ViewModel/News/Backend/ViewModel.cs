using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.News.Backend
{
    public class BackendNewsViewModel : BackendBaseViewModel
    {
        public BackendNewsViewModelItem Data { get; set; }

        public BackendNewsViewModel()
        {
            Data = new BackendNewsViewModelItem();
        }
    }

    public class BackendNewsViewModelItem : BaseViewModelItem
    {
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public String Image { get; set; }
        public String Text { get; set; }
        public String Link { get; set; }

        public BackendNewsViewModelItem()
        {

        }
    }
}