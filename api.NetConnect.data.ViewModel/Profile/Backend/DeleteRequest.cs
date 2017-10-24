using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Profile.Backend
{
    public class BackendProfileDeleteRequest
    {
        public List<Int32> IDs { get; set; }

        public BackendProfileDeleteRequest()
        {
            IDs = new List<Int32>();
        }
    }
}
