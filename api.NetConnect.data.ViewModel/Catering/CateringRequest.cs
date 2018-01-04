using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Catering
{
    public class OrderRequest
    {
        public Int32 UserID { get; set; }
        public Int32 SeatID { get; set; }
        public Int32 EventID { get; set; }
        public List<OrderRequestItem> Items { get; set; }

        public OrderRequest()
        {
            Items = new List<OrderRequestItem>();
        }

        public class OrderRequestItem
        {
            public Int32 ID { get; set; }
            public String Attributes { get; set; }
            public Int32 Amount { get; set; }
        }
    }
}
