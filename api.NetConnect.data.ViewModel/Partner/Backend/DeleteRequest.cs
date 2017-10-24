using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Partner.Backend
{
    public class BackendPartnerDeleteRequest
    {
        public List<Int32> IDs { get; set; }

        public BackendPartnerDeleteRequest()
        {
            IDs = new List<Int32>();
        }
    }
}
