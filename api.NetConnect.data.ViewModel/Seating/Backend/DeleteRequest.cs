using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Seating.Backend
{
    public class BackendSeatingDeleteRequest
    {
        public List<Int32> IDs { get; set; }

        public BackendSeatingDeleteRequest()
        {
            IDs = new List<Int32>();
        }
    }
}
