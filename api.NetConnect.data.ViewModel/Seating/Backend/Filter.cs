using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Seating.Backend
{
    public partial class BackendSeatingFilter
    {
        public SeatingFilterEvent EventSelected { get; set; }
        public List<SeatingFilterEvent> EventOptions { get; set; }

        public BackendSeatingFilter()
        {

        }
    }

    public class SeatingFilterEvent
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
    }
}
