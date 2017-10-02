using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Partner
{
    class PartnerViewModel : BaseViewModel
    {
        public PartnerViewModelItem Data { get; set; }
        public PartnerViewModel()
        {
            Data = new PartnerViewModelItem();
        }
    }
    public class PartnerViewModelItem : BaseViewModelItem
    {

    }
}
