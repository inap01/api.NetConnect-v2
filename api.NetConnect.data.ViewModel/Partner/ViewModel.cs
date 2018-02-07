using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Partner
{
    public class PartnerListViewModel : ListViewModel<PartnerViewModelItem>
    {
        public PartnerListViewModel() : base()
        {

        }
    }

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
        public List<PartnerDisplay> Display { get; set; }

        public PartnerViewModelItem()
        {
            PartnerType = new PartnerType();
            Display = new List<PartnerDisplay>();
        }
    }

    public class PartnerDisplay
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public Boolean Value { get; set; }
    }

    public class PartnerType
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
    }
}