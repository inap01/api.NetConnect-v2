using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Info
{
    public class InfoViewModel : BaseViewModel
    {
        public InfoViewModelItem Data { get; set; }

        public InfoViewModel()
        {
            Data = new InfoViewModelItem();
        }
    }

    public class InfoViewModelItem : BaseViewModelItem
    {
        public Double ReservationCost { get; set; }
        public String Location { get; set; }
        public String Street { get; set; }
        public String Postcode { get; set; }
        public String RouteLink { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Int32 MinAge { get; set; }
    }
}
