using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Seating.Backend
{
    public partial class SeatingFilter
    {
        public String SeatNumber { get; set; }
        public String Name { get; set; }
        public String UsageString { get; set; }
        public SeatingStatusFilter Status { get; set; }

        public SeatingFilter()
        {
            SeatNumber = "";
            Name = "";
            UsageString = "";
            Status = SeatingStatusFilter.Ungefiltert;
        }
    }

    public enum SeatingStatusFilter { Ungefiltert, Frei, Vorgemerkt, Reserviert, NetConnect }
}
