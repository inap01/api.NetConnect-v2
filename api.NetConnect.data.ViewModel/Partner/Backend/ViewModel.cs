using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Partner.Backend
{
    public class BackendPartnerViewModel : BackendBaseViewModel
    {
        public BackendPartnerViewModelItem Data { get; set; }
        public List<PartnerType> PartnerTypeOptions { get; set; }

        public BackendPartnerViewModel()
        {
            Data = new BackendPartnerViewModelItem();
            PartnerTypeOptions = new List<PartnerType>();
        }
    }

    public class BackendPartnerViewModelItem : BaseViewModelItem
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public String Link { get; set; }
        public String RefLink { get; set; }
        public PartnerType PartnerTypeSelected { get; set; }
        public List<PartnerDisplay> Display { get; set; }
        public Boolean IsActive { get; set; }

        public BackendPartnerViewModelItem()
        {
            PartnerTypeSelected = new PartnerType();
            Display = new List<PartnerDisplay>();
        }
    }

    public class BackendPartnerPositionViewModel : BaseViewModel
    {
        public List<BackendPartnerPositionViewModelItem> Data { get; set; }
        public List<String> PartnerTypeOptions { get; set; }

        public BackendPartnerPositionViewModel()
        {
            Data = new List<BackendPartnerPositionViewModelItem>();
        }

        public class BackendPartnerPositionViewModelItem : BaseViewModelItem
        {
            public String Name { get; set; }
            public Int32 Position { get; set; }
        }
    }
}