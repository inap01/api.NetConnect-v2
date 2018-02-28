using api.NetConnect.data.ViewModel.Event.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Catering.Backend
{
    public class BackendNewOrderRequest
    {
        public List<BackendNewOrderRequestItem> Data { get; set; }
        public BackendEventViewModelItem Event { get; set; }
        public String Note { get; set; }
    }

    public class BackendNewOrderRequestItem
    {
        public Int32 Amount { get; set; }
        public String[] Attributes { get; set; }
        public BackendCateringProductItem Product { get; set; }
    }
}
