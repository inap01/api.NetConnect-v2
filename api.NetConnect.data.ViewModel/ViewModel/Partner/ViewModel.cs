using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels.Profile
{
    public class PartnerViewModel : BaseViewModel
    {
        public PartnerViewModelItem Data { get; set; }
        public Int32 TotalCount { get; set; }

        public PartnerViewModel()
        {
            Data = new PartnerViewModelItem();
            TotalCount = 0;
        }
    }

    public class PartnerViewModelItem
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Link { get; set; }
        public String PartnerTitle { get; set; }
        public String ImagePath { get; set; }
        public String Icon { get; set; }
        public String ImageAlt { get; set; }
        public String State { get; set; }
        public Boolean IsActive { get; set; }
        public Int32 Position { get; set; }

        public PartnerViewModelItem()
        {
        }
    }
}