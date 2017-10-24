using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Partner
{
    public class PartnerViewModel : BaseViewModel
    {
        public PartnerViewModelItem Data { get; set; }

        public PartnerViewModel()
        {
            Data = new PartnerViewModelItem();
        }
    }

    public class PartnerViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public String Link { get; set; }
        public String RefLink { get; set; }
        public PartnerType PartnerType { get; set; }
        public Dictionary<String, Boolean> Display { get; set; }

        public PartnerViewModelItem()
        {
            PartnerType = new PartnerType();
            Display = new Dictionary<string, bool>();
        }
    }

    public class PartnerType
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
    }
}