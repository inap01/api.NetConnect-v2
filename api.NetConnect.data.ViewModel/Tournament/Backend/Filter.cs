using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Tournament.Backend
{
    public class BackendTournamentFilter
    {
        public String Volume { get; set; }
        public String Game { get; set; }
        public String Day { get; set; }

        public BackendTournamentFilter()
        {
            Volume = "";
            Game = "";
            Day = "";
        }
    }
}
