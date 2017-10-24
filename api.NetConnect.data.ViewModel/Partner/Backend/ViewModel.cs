using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Partner.Backend
{
    public class BackendPartnerViewModel : BackendBaseViewModel
    {
        public BackendPartnerViewModelItem Data { get; set; }

        public BackendPartnerViewModel()
        {
            Data = new BackendPartnerViewModelItem();
        }
    }

    public class BackendPartnerViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public String Link { get; set; }
        public String RefLink { get; set; }
        public PartnerType PartnerType { get; set; }
        public Dictionary<String, Boolean> Display { get; set; }

        public BackendPartnerViewModelItem()
        {
            PartnerType = new PartnerType();
            Display = new Dictionary<string, bool>();
        }
    }
}