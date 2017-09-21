using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Tournament
{
    public class TournamentFilter
    {
        public String Volume { get; set; }
        public String Game { get; set; }
        public String Day { get; set; }

        public TournamentFilter()
        {
            Volume = "";
            Game = "";
            Day = "";
        }
    }
}
