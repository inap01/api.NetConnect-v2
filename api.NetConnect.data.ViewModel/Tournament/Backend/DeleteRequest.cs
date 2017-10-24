using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Tournament.Backend
{
    public class BackendTournamentDeleteRequest
    {
        public List<Int32> IDs { get; set; }

        public BackendTournamentDeleteRequest()
        {
            IDs = new List<Int32>();
        }
    }
}
