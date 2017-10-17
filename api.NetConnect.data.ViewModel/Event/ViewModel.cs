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
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public String Image { get; set; }
        public String Text { get; set; }

        public EventViewModelItem()
        {

        }
    }
}